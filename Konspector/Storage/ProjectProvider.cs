using System.Xml.Serialization;
using Color = System.Drawing.Color;
namespace Konspector.Storage;

//to be used in DI with configurable storage providers
public class ProjectProvider{
    private string _path;
    public Project project;
    public ProjectProvider(string path){
        _path = path;
        
        if(File.Exists(_path))
            loadProject();
        else
            createProject();
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

}