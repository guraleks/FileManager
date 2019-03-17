using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager
{
    // Класс, реализующий методы для локальной работы с файлами 
    public class LocalFileManager : IFileManager
    {
        private const string separator = "_version_";

        public void CreateFile(string path)
        {
            if (File.Exists(path))
            {
                UpdateFile(path);
            }
            else
            {
                File.Create(path);
            }
        }

        
        public void CreateFile(string srcPath, string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    UpdateFile(srcPath, path);
                }
                else
                {
                    CopyContent(srcPath, path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No such file in source directory");
            }
        }

        
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        
        public void UpdateFile(string path)
        {
            CreateFile(IncrementVersion(path));
        }

        
        public void UpdateFile(string srcPath, string path)
        {
            CreateFile(srcPath, IncrementVersion(path));
        }

        
        public string GetFile(string path)
        {
            var list = new List<string>();
            
            foreach (var fileName in Directory.GetFiles(ExtractDirectoryNameFromPath(path)))
            {
                var fileNameNoExt = ExtractFileNameFromPath(fileName, false).Split(separator)[0];
                if (fileNameNoExt.Equals(ExtractFileNameFromPath(path, false)))
                {
                    list.Add(fileName);
                }
            }

            switch (list.Count)
            {
                case 0:
                    return "Such file does not exist";
                case 1:
                    return list[0];
            }

            var version = 0;
            var num = 0;
            for (var i = 0; i < list.Count; i++)
            {
                try
                {
                    if (Convert.ToInt32(ExtractFileNameFromPath(list[i], true).Split(separator)[1]) > version)
                    {
                        version = Convert.ToInt32(ExtractFileNameFromPath(list[i], true).Split(separator)[1]);
                        num = i;
                    }
                }
                catch (Exception e) {}
            }

            return list[num];
        }

        
        public void CopyContent(string srcPath, string destPath)
        {
            FileInfo fileName = new FileInfo(srcPath);
            fileName.CopyTo(destPath, true);
        }


        private string IncrementVersion (string path)
        {
            var words = path.Split(separator);
            
            if (words.Length > 1)
            {
                var version = 0;
                version = Convert.ToInt32(words[1].Split('.')[0]);
                version++;
                path = words[0] + separator + version.ToString() + '.' + words[1].Split('.')[1];
            }
            else
            {
                path = path.Split('.')[0] + separator + "1." + path.Split('.')[1];
            }

            return path;
        }

        
        private string ExtractDirectoryNameFromPath(string path)
        {
            var str = "";
            var strings = path.Split('/');
            Array.Resize(ref strings, strings.Length - 1);
            foreach (var s in strings)
            {
                str += s;
                str += "/";
            }
            return str;
        }
       

        private string ExtractFileNameFromPath(string path, bool withVersion)
        {
            if (withVersion)
            {
                var arr = path.Split('/');
                return arr.Last().Split('.')[0];
            }
            else
            {
                var arr = path.Split('/');
                return arr.Last().Split('.')[0].Split(separator)[0];
            }
        }


        private string ExtractExtansionFromPath(string path)
        {
            return path.Split('.').Last();
        }
    }
}