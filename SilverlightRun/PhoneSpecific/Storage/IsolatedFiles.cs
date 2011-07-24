using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;

namespace SilverlightRun.PhoneSpecific.Storage
{
    /// <summary>
    /// Allows simple file IO on IS files.
    /// </summary>
    public class IsolatedFiles
    {
        /// <summary>
        /// Basic file representation for IS.
        /// </summary>
        public class File
        {
            public string Content { get; set; }
            public string FileName { get; set; }
        }

        public static void EnsureUniqueness(string newName, Action act)
        {
            if (IsUniqueName(newName))
            {
                act();
            }
            else
            {
                MessageBox.Show("A file with the name \"" + newName + "\" already exists.", "Information", MessageBoxButton.OK);
            }
        }

        public static void EnsureValidOperation(Action act)
        {
            try
            {
                act();
            }
            catch (Exception)
            {
                MessageBox.Show("The action was not valid.", "Information", MessageBoxButton.OK);
            }
        }

        public static bool IsUniqueName(string name)
        {
            bool isUnique = true;
            OnAllFiles((_, files) =>
            {
                isUnique = files.All((f) => f != name);
            });
            return isUnique;
        }

        public static IEnumerable<File> All()
        {
            var allfiles = new List<File>();
            OnAllFiles((istorage, files) =>
            {
                foreach (var file in files)
                    GetFile(istorage, file, (bm) => allfiles.Add(bm));
            });
            return allfiles.OrderBy((bm) => bm.FileName).ToList();
        }

        public static void Save(File file)
        {
            using (var istorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                WriteFileToFile(file, istorage, GetFilePath(file));
            }
        }

        public static void Delete(File file)
        {
            using (var istorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (istorage.FileExists(file.FileName))
                    istorage.DeleteFile(file.FileName);
            }
        }

        private static void OnAllFiles(Action<IsolatedStorageFile, string[]> returns)
        {
            using (var istorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                returns(istorage, istorage.GetFileNames());
            }
        }

        private static void GetFile(IsolatedStorageFile istorage, string filename, Action<File> returns)
        {
            using (var openedFile = istorage.OpenFile(filename, System.IO.FileMode.Open))
            {
                ReadFileFromOpenFile(filename, openedFile, returns);
            }
        }

        private static void ReadFileFromOpenFile(string filename, IsolatedStorageFileStream openedFile, Action<File> returns)
        {
            using (var sreader = new StreamReader(openedFile))
            {
                returns(new File()
                {
                    Content = sreader.ReadToEnd(),
                    FileName = filename
                });
            }
        }

        private static void WriteFileToFile(File file, IsolatedStorageFile istorage, string newFilePath)
        {
            using (var newFile = istorage.CreateFile(newFilePath))
            {
                using (var newWriteableFile = new StreamWriter(newFile, Encoding.Unicode))
                {
                    newWriteableFile.Write(file.Content);
                    newWriteableFile.Flush();
                }
            }
        }

        private static string GetFilePath(File file)
        {
            return file.FileName.Replace(":", "#");
        }
    }
}
