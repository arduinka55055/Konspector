using System.Xml.Serialization;

namespace Konspector.Storage
{
    public class MarkdownItem: ItemBase
    {
        [XmlText]
        public string? Content { get; set; }
        [XmlAttribute("href")]
        public string? href { get; set; }
    }
}