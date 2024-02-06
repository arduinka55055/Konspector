using System.Text.Json.Nodes;
using Konspector.Interfaces;
using Konspector.Services;
using System.Text.Json;

namespace TestBlazorApp1;

public class AITests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        ITextGenerator generator = new OllamaGenerator();
        string prompt = "What is the meaning of life?";
        string text = generator.GenerateText(prompt, CancellationToken.None).Result;
        Console.WriteLine(text);
        Assert.IsNotNull(text);
        Assert.That(text, Is.Not.EqualTo("Error"));
    }
}