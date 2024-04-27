//maui properties
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Konspector.Models;
using System.Diagnostics;
namespace Konspector.Misc;

public class Settings(ILoggerFactory logger)
{
    public ILogger logger = logger.CreateLogger<Settings>();

    public string AppName => "Konspector";
    public string AppDirectory {
        get {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + AppName;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }

    public string DocumentsDirectory {
        get {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + AppName;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
    public SettingsModel GetAppSettings() { 
        string settingsPath = AppDirectory + "\\settings.json";
        if (File.Exists(settingsPath)) {
            string json = File.ReadAllText(settingsPath);
             SettingsModel? sm = JsonSerializer.Deserialize<SettingsModel>(json);
            if(sm == null){
                logger.LogError($"Failed to read settings from file, probably invalid json: {settingsPath}");
                //crash the app
                throw new Exception("Failed to read settings from file");
            }
            return sm;
        } else {
            return new SettingsModel{
                DefaultNoteDirectory = DocumentsDirectory,
            };
        }
    }
    public void Save( SettingsModel value) {
        string settingsPath = AppDirectory + "\\settings.json";
        string json = JsonSerializer.Serialize(value);
        File.WriteAllText(settingsPath, json);
    }

}