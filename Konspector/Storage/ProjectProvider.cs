using System.Xml.Serialization;
using Konspector.Misc;
using Color = System.Drawing.Color;
namespace Konspector.Storage;

//to be used in DI with configurable storage providers
public class ProjectProvider{
    private string _path;
    public Project project;
    public ProjectProvider(Settings settings){
        _path = settings.AppDirectory + "\\project.xml";
        
        if(File.Exists(_path))
            loadProject();
        else
            createProject();
            
        setParent();
    }
    public void createProject(){
        project = new Project();
        //add default items
        project.Kanbans.Add(new KanbanColumn{Title = "Getting started",color = Color.Red});
        project.Kanbans.Add(new KanbanColumn{Title = "In progress",color = Color.Yellow});

        project.Kanbans[0].Items.Add(new KanbanItem{Title = "Learn our app",color = Color.FromArgb(0,255,0)});
        project.Kanbans[0].Items.Add(new KanbanItem{Title = "Create a project",color = Color.FromArgb(255,165,0)});
        project.Kanbans[0].Items.Add(new KanbanItem{Title = "Add some notes",color = Color.FromArgb(255,165,0)});
        project.Kanbans[0].Items.Add(new KanbanItem{Title = "Add some references",color = Color.FromArgb(255,165,0)});
        project.Kanbans[1].Items.Add(new KanbanItem{Title = "Stay cool!",color = Color.FromArgb(0,255,0)});

        project.Todos.Add(new TodoList{Title = "Learn our app",color = Color.FromArgb(0,255,0)});
        project.Todos[0].Items.Add(new TodoItem{Title = "Opened the app",color = Color.FromArgb(0,255,0), IsDone = true});
        project.Todos[0].Items.Add(new TodoItem{Title = "Create a project",color = Color.FromArgb(255,165,0)});
        project.Todos[0].Items.Add(new TodoItem{Title = "Add some notes",color = Color.FromArgb(255,165,0)});
        project.Todos[0].Items.Add(new TodoItem{Title = "Add some references",color = Color.FromArgb(255,165,0)});
        project.Todos[0].Items.Add(new TodoItem{Title = "Stay cool!",color = Color.FromArgb(0,255,0)});

        project.Notes.Add(new MarkdownItem{Title = "Welcome to Konspector", Content = "Welcome to Konspector, the best note taking app in the world!"});
        
        project.Refs.Add(new Ref{Src = project.Kanbans[0].Id, Dst = project.Todos[0].Id});
        project.Refs.Add(new Ref{Src = project.Todos[0].Id, Dst = project.Notes[0].Id});
        saveProject();

    }
    public void loadProject(){
        XmlSerializer serializer = new XmlSerializer(typeof(Project));
        using var reader = new StreamReader(_path);
        object? res = serializer.Deserialize(reader);
        if(res is Project p)
            project = p;
        else 
            throw new Exception("Failed to load project");
    }

    public void saveProject(){
        XmlSerializer serializer = new XmlSerializer(typeof(Project));
        using var writer = new StreamWriter(_path);
        serializer.Serialize(writer, project);
    }

    private void setParent(){
        setParent(project);
    } 
    //recursive rewrite of setParent
    private void setParent(object obj, uint depth = 0){
        if(depth > 10) return;
        if(obj is ItemBase ib)
            ib._root = this;
        foreach(var prop in obj.GetType().GetProperties()){
            if(prop.PropertyType.IsGenericType){
                if (prop.GetValue(obj) is not IEnumerable<object> listObj) continue;
                foreach (var item in listObj){
                    setParent(item, depth + 1);
                }
            }
        }
    }

    public ItemBase? GetElementById(Guid dst)
    {
        return GetElementById(project, dst);
    }
    internal ItemBase? GetElementById(object obj, Guid dst)
    {
        if(obj is ItemBase ib && ib.Id == dst)
            return ib;
        foreach(var prop in obj.GetType().GetProperties()){
            if(prop.PropertyType.IsGenericType){
                if (prop.GetValue(obj) is not IEnumerable<object> listObj) continue;
                foreach (var item in listObj){
                    var res = GetElementById(item, dst);
                    if(res != null)
                        return res;
                }
            }
        }
        return null;
    }
}