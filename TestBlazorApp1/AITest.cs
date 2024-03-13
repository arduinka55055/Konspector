using System.Text.Json.Nodes;
using Konspector.Interfaces;
using Konspector.Services;
using System.Text.Json;

namespace TestBlazorApp1;

[TestFixture]
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
    [Test]
    public void Test2()
    {
        Konspector.Services.GDrive.Run().Wait();
        Assert.Pass();
    }
    [Test]
    public void TestAiWIthContext(){
        ITextGenerator generator = new OllamaGenerator();
        string prompt = "Про що була лекція 8 лютого 2024?";
        string content = File.ReadAllText("../../../Content.txt");//bin/debug/net
        string text = generator.GenerateText(prompt, CancellationToken.None, content).Result;
        Console.WriteLine(text);
        Assert.IsNotNull(text);
        Assert.That(text, Is.Not.EqualTo("Error"));
        //це вже інтеграційний тест має це робити, коментуємо подалі
        //File.WriteAllText("../../../GeneratedText.txt", text);
        //Assert.That(text, Does.Contain("Розробка"));   
    }
}