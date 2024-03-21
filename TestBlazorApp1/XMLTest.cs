using System.Text.Json.Nodes;
using Konspector.Interfaces;
using Konspector.Services;
using System.Text.Json;
using Konspector.Storage;
using Microsoft.Maui.Platform;

namespace TestBlazorApp1;

[TestFixture]
public class XMLTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCreateAndSave(){
        ProjectProvider provider = new("../../../TestProject.xml");
        provider.saveProject();
        //check if file exists
        Assert.That(File.Exists("../../../TestProject.xml"), Is.True);
    }
    [Test]
    public void TestLoad(){
        ProjectProvider provider = new ProjectProvider("../../../TestProject.xml");
        Assert.That(provider.project, Is.Not.Null);
        Assert.That(provider.project.Kanbans.Count, Is.AtLeast(1));
    }
    
}