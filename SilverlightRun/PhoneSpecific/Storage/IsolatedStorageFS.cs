using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Media.Imaging;

namespace SilverlightRun.PhoneSpecific.Storage
{
    /// <summary>
    /// Provides basic file IO on isolated storage.
    /// </summary>
    public class IsolatedStorageFS : IFileSystem
    {
        IsolatedStorageFile _is;

        public IsolatedStorageFS()
        {
            _is = IsolatedStorageFile.GetUserStoreForApplication();
        }

        private string FileName(string folder, string file)
        {
            return folder + "\\" + file;
        }

        public string[] Folders()
        {
            return _is.GetDirectoryNames();
        }

        public string[] Files(string folder)
        {
            return _is.GetFileNames(folder + "\\*");
        }

        public string File(string folder, string file)
        {
            using (var fileStream = new StreamReader(_is.OpenFile(FileName(folder, file), FileMode.Open)))
            {
                return fileStream.ReadToEnd();
            }
        }

        public void NewFolder(string folder)
        {
            _is.CreateDirectory(folder);
        }

        public void NewFile(string folder, string file)
        {
            NewFile(folder, file, "");
        }

        public void NewFile(string folder, string file, string content)
        {
            using (var fileStream = new StreamWriter(_is.CreateFile(FileName(folder, file))))
            {
                fileStream.Write(content);
            }
        }

        public void WriteFile(string folder, string file, string content)
        {
            using (var fileStream = new StreamWriter(_is.OpenFile(FileName(folder, file), FileMode.Create)))
            {
                fileStream.Write(content);
            }
        }

        public void DeleteFolder(string folder)
        {
            foreach (var file in Files(folder)) _is.DeleteFile(FileName(folder, file));
            _is.DeleteDirectory(folder);
        }

        public void DeleteFile(string folder, string file)
        {
            _is.DeleteFile(FileName(folder, file));
        }

        public bool ContainsFolder(string folder)
        {
            return Folders().Contains(folder);
        }

        public bool ContainsFile(string folder, string file)
        {
            return Files(folder).Contains(file);
        }

        public void CopyFolder(string folder, string newName)
        {
            NewFolder(newName);
            foreach (var file in Files(folder)) CopyFile(folder, file, newName, file);
        }

        public void CopyFile(string folder, string file, string newFolder, string newName)
        {
            string content = File(folder, file);
            NewFile(newFolder, newName, content);
        }

        public void SaveImage(string folder, string file, BitmapImage image)
        {
            using (var fileStream = _is.OpenFile(FileName(folder, file), FileMode.Create))
            {
                var wImg = new WriteableBitmap(image);
                wImg.SaveJpeg(fileStream, wImg.PixelWidth, wImg.PixelHeight, 0, 85);
            }
        }

        public BitmapImage LoadImage(string folder, string file)
        {
            using (var fileStream = _is.OpenFile(FileName(folder, file), FileMode.Open))
            {
                var bImg = new BitmapImage();
                bImg.SetSource(fileStream);
                return bImg;
            }
        }
    }
}
