//basic XML serialization/deserialization and many to many junctions
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Konspector.Storage;

public class ItemBase {
    [XmlIgnore]
    internal ProjectProvider _root;
    [XmlAttribute("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    [XmlAttribute("title")]
    public string Title { get; set; } = "Title";
    //we want to store this
    [XmlAttribute("color")]
    public string Color
    {
        get
        {
            return System.Drawing.ColorTranslator.ToHtml(color);
        }
        set
        {
            color = System.Drawing.ColorTranslator.FromHtml(value);
        }
    }
    //we want to access this
    [XmlIgnore]
    public System.Drawing.Color color { get; set; } = System.Drawing.Color.FromArgb(0, 0, 0);


    public List<ItemBase> GetReferences()
    {
        return _root.project.Refs.FindAll(r => r.Src == Id).ConvertAll(r => _root.GetElementById(r.Dst)!);
    }    
}