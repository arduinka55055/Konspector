using Konspector.Misc;

namespace Konspector.Models;

public class SettingsModel
{
    // General settings
    public string? DefaultNoteDirectory { get; set; }
    public bool AutoSaveEnabled { get; set; }
    public bool ImmediateSaveEnabled { get; set; }
    public int AutoSaveIntervalMinutes { get; set; }

    // Sync settings
    public bool GoogleDriveSyncEnabled { get; set; }
    public string? GoogleDriveSyncFolder { get; set; }
    public bool FtpSyncEnabled { get; set; }
    public string? FtpConnectionString { get; set; }

    // AI settings
    public bool AiAssistanceEnabled { get; set; }
    public bool AiUseContextEnabled { get; set; }
    public string AiServer{ get; set; } = "https://coursework.sudohub.dev/";
    public string AiPassword { get; set; } = "password";
    public string AiLanguage { get; set; } = "English";

    // Markdown settings
    public bool MarkdownSupportEnabled { get; set; }
    public bool MarkdownPreviewEnabled { get; set; }

    // Other settings
    public bool DarkModeEnabled { get; set; }
    public int FontSize { get; set; } = 12;
    public string DefaultEditor { get; set; } = "Notepad";
}
