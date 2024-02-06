//an interface for text generation using ChatGPT, google or other services
namespace Konspector.Interfaces
{
    public interface ITextGenerator
    {
        public string Provider { get; }
        public string ModelName { get; set; }
        public string NegativePrompt { get; set; }
        public Task<string> GenerateText(string prompt, CancellationToken token, int length = 200);
    }
}