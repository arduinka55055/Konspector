using System.Xml.Serialization;

namespace Konspector.Storage;

public class TodoItem:ItemBase
{
    [XmlAttribute("isDone")]
    public bool IsDone { get; set; }
}

public class TodoList:ItemBase
{
    [XmlArray("items")]
    [XmlArrayItem("item")]
    public List<TodoItem> Items { get; set; } = [];
}