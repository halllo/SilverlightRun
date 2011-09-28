using System.Windows.Media.Imaging;

namespace SilverlightRun.PhoneSpecific.Storage
{
    /// <summary>
    /// Provides basic file IO for abstract storages.
    /// </summary>
    public interface IFileSystem
    {
        string[] Folders();
        string[] Files(string folder);
        string File(string folder, string file);
        void NewFolder(string folder);
        void NewFile(string folder, string file);
        void NewFile(string folder, string file, string content);
        void WriteFile(string folder, string file, string content);
        void DeleteFolder(string folder);
        void DeleteFile(string folder, string file);
        bool ContainsFolder(string folder);
        bool ContainsFile(string folder, string file);
        void CopyFolder(string folder, string newName);
        void CopyFile(string folder, string file, string newFolder, string newName);
        void SaveImage(string folder, string file, BitmapImage image);
        BitmapImage LoadImage(string folder, string file);
    }
}
