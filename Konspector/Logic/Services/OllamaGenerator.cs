using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Konspector.Interfaces;

namespace Konspector.Services
{
    public class OllamaGenerator : ITextGenerator
    {
        public string Provider => "Ollama";
        public string ModelName { get; set; } = "wizard-vicuna-uncensored";
        public string NegativePrompt { get; set; } = "I am";
        public async Task<string> GenerateText(string prompt, CancellationToken token, int length = 200)
        {
            /*
            curl http://localhost:11434/api/generate -d '{
            "model": "llama2",
            "prompt":"Why is the sky blue?"
            }'
            */
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync("http://localhost:11434/api/generate", new
            { 
                model = ModelName, 
                prompt = prompt,
                stream = false,
            }, token);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                string text = JsonSerializer.Deserialize<JsonElement>(result).GetProperty("response").GetString() ?? "Error";
                return text;
            }
            else
            {
                return "Error";
            }

        }
    }
}