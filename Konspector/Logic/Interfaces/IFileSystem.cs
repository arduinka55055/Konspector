namespace Konspector.Interfaces;
//a basic filesystem interface using async methods streams and cancellation tokens
public interface IFileSystem
{
    //read write ops
    Stream ReadFile(string filePath);
    void WriteFile(string filePath, Stream content);
    //delete file
    void DeleteFile(string filePath);
    //copy file
    void CopyFile(string sourcePath, string destinationPath);
    //move file
    void MoveFile(string sourcePath, string destinationPath);
    
    //directory handling
    void CreateDirectory(string directoryPath);
    void DeleteDirectory(string directoryPath);
    void MoveDirectory(string sourcePath, string destinationPath);
    void CopyDirectory(string sourcePath, string destinationPath);
    //list files in directory
    string[] ListFiles(string directoryPath);
    //list directories in directory
    string[] ListDirectories(string directoryPath);
    //check if file exists
    bool FileExists(string filePath);
    //check if directory exists
    bool DirectoryExists(string directoryPath);
    //get file info
    FileInfo GetFileInfo(string filePath);
    //get directory info
    DirectoryInfo GetDirectoryInfo(string directoryPath);
    //get file size
    long GetFileSize(string filePath);
}