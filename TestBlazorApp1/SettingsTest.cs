//create read write settings

using System.Text.Json.Nodes;
using Konspector.Interfaces;
using Konspector.Services;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Konspector.Misc;

namespace TestBlazorApp1;

public class SettingsTests
{
    MauiAppBuilder builder = MauiApp.CreateBuilder();
    MauiApp app;
    [SetUp]
    public void Setup()
    {
        //mock DI
        ConfigureApp.Configure(builder);
        app = builder.Build();
    }

    [Test]
    public void Test1()
    {
        Settings settings = app.Services.GetRequiredService<Settings>();
        Assert.That(settings, Is.Not.Null);
        Assert.That(settings.DocumentsDirectory, Is.Not.Null);
        Assert.That(settings.AppDirectory, Is.Not.Null);
        Assert.That(settings.AppName, Is.Not.Null);
        //try to write settings
        SettingsModel sm = settings.GetAppSettings();
        sm.AiLanguage = "Ukrainian";
        //save settings
        settings.Save(sm);
        Assert.That(sm.AiLanguage, Is.EqualTo("Ukrainian"));
        //get settings again from DI and check if it was saved
        Settings settings2 = app.Services.GetRequiredService<Settings>();
        Assert.That(settings2.GetAppSettings().AiLanguage, Is.EqualTo("Ukrainian"));
    }
}