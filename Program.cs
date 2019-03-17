using System;
using System.IO;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileManager fm = new LocalFileManager();
            fm.CreateFile("/Users/aleksei/1.doc");
            Console.WriteLine(fm.GetFile("/Users/aleksei/1.doc"));
            fm.CreateFile("/Users/aleksei/1.doc");
            Console.WriteLine(fm.GetFile("/Users/aleksei/1.doc"));
            fm.CreateFile("/Users/2.doc", "/Users/aleksei/1.doc");
            fm.DeleteFile("/Users/aleksei/1_version_2.doc");
        }
    }
}