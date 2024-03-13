using System.Xml.Serialization;

namespace Konspector.Storage;

public class KanbanItem:ItemBase
{
    [XmlAttribute("description")]
    public string Description { get; set; } = "";
    [XmlAttribute("priority")]
    public int Priority { get; set; }
    [XmlAttribute("deadline")]
    public DateTime Deadline { get; set; }
}

public class KanbanColumn:ItemBase
{
    //items are stored inside tag
    [XmlArray("items")]
    [XmlArrayItem("item")]
    public List<KanbanItem> Items { get; set; } = [];
}