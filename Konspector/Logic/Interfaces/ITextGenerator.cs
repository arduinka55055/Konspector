//an interface for text generation using ChatGPT, google or other services
using System.Text;

namespace Konspector.Interfaces
{
    public interface ITextGenerator
    {
        public string Provider { get; }
        public string ModelName { get; set; }
        public string NegativePrompt { get; set; }
        public Task<string> GenerateText(string prompt, CancellationToken token, string? context = null, int length = 200);
        
        //streaming response
        public Task GenerateTextS(string prompt, Func<StringBuilder, Task> callback, CancellationToken token, string? context = null);
    }
}