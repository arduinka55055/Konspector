using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Konspector.Interfaces;

namespace Konspector.Services
{
    public class OllamaGenerator : ITextGenerator
    {
        public static readonly string PROMPT_EN = "You're a personal note-taking companion. I want you to use the following lecture material and answer my questions about it.\n\n Here's the material you need to know:\n";
        public static readonly string PROMPT_UA = "Ти мій персональний помічник з нотаток. Я хочу, щоб ти використовував наступний лекційний матеріал та відповідав на мої питання про нього. Притримуйся матеріалу, але не уникай додаткових розʼяснень. Відповідай українською завжди.\n\n Ось матеріал, який тобі потрібно знати:\n";
        public string Provider => "Ollama";
        public string ModelName { get; set; } = "llama2:13b";//"wizard-vicuna-uncensored";
        public string NegativePrompt { get; set; } = "I am";
        private object PrepareRequest(string prompt, string? context = null, int length = 200, bool stream = false)
        {
            /*
            curl http://localhost:11434/api/generate -d '{
                "model": "llama2",
                "system":"You're a cool AI",
                "prompt":"Why is the sky blue?"
            }'
            */
            string? sys = PROMPT_EN;//PROMPT_EN; TODO: move to settings
            if (context != null)
            {
                sys += context;
            }
            else{
                sys = null;
            }
            return new
            {
                model = ModelName,
                system = sys,
                prompt = prompt,
                stream = stream,
            };
        }
        public async Task<string> GenerateText(string prompt, CancellationToken token, string? context = null, int length = 200)
        {
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync("http://localhost:11434/api/generate", PrepareRequest(prompt, context, length), token);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                string text = JsonSerializer.Deserialize<JsonElement>(result).GetProperty("response").GetString() ?? "Error";
                return text;
            }
            else
            {
                return "Error " + response.StatusCode;
            }
        }
        //streaming response, cycle until ollama ends response or token is cancelled
        public async Task GenerateTextS(string prompt, Func<StringBuilder, Task> callback, CancellationToken token, string? context = null)
        {
            try
            {
                var text = new StringBuilder(500);
                var req = PrepareRequest(prompt, context, stream: true);
                using var httpContent = new StringContent(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");
                using var client = new HttpClient();
                using var message = new HttpRequestMessage(HttpMethod.Post, "http://localhost:11434/api/generate");
                message.Content = httpContent;
                using var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, token);
                using var reader = new StreamReader(await response.Content.ReadAsStreamAsync(token));
                while (!reader.EndOfStream)
                {
                    if (token.IsCancellationRequested)
                    {
                        text.Append("[Cancelled]");
                        await callback(text);
                        return;
                    }

                    string? line = reader.ReadLine();
                    if (line != null)
                    {
                        var json = JsonSerializer.Deserialize<JsonElement>(line);
                        string newText = json.GetProperty("response").GetString() ?? "Error";
                        text.Append(newText);
                        await callback(text);
                        if (json.TryGetProperty("done", out var doneProperty) && doneProperty.ValueKind == JsonValueKind.True)
                        {
                            break; // Exit the loop if generation is done
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await callback(new StringBuilder("Error: " + ex.Message));
            }
        }
    }
}