using System.Xml;
using System.Xml.Serialization;

namespace Konspector.Storage;


/*
<?xml version="1.0" encoding="utf-8"?>
<data user="test">

    <kanbans>
        <kanbancolumn id="1" title="Create" color="#009688">
            <kanbanitem id="1" title="Task 1" description="Task 1 description" color="#009688" />
            <kanbanitem id="2" title="Task 2" description="Task 2 description" color="#009688" />
            <kanbanitem id="3" title="Task 3" description="Task 3 description" color="#009688" />
        </kanbancolumn>
    </kanbans>

    <todos>
        <todolist id="1" title="To Do" color="#009688">
            <todoitem id="1" title="Task 1" description="Task 1 description" color="#009688" />
            <todoitem id="2" title="Task 2" description="Task 2 description" color="#009688" />
            <todoitem id="3" title="Task 3" description="Task 3 description" color="#009688" />
        </todolist>
    </todos>

    <notes>
        <note id="1" title="Note 1" description="Note 1 description" color="#009688">
            # hello
            - world
            **in Markdown**
        </note>
        <note id="2" title="Note 2" description="Note 2 description" color="#009688" href="https://www.google.com" />
    </notes>

    <refs>
        <ref src="1" dst="2" />
    </refs>
</data>

*/

public class Ref
{
    [XmlAttribute("src")]
    public Guid Src { get; set; }
    [XmlAttribute("dst")]
    public Guid Dst { get; set; }
}

public class Project
{
    private XmlDocument _root;
    [XmlAttribute("user")]
    public string User { get; set; } = "guest";
    [XmlArray("kanbans")]
    [XmlArrayItem("kanbancolumn")]
    public List<KanbanColumn> Kanbans { get; set; } = [];
    [XmlArray("todos")]
    [XmlArrayItem("todolist")]
    public List<TodoList> Todos { get; set; } = [];
    [XmlArray("notes")]
    [XmlArrayItem("note")]
    public List<MarkdownItem> Notes { get; set; } = [];
    [XmlArray("refs")]
    [XmlArrayItem("ref")]
    public List<Ref> Refs { get; set; } = [];
    
}