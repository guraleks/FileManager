namespace FileManager
{
    // Для реализации другого типа хранения данных
    // необходимо имплементировать этот интерфейс
    public interface IFileManager
    {
        void CreateFile (string path);
        
        // Перегруженная версия предыдущего метода:
        // файл создается и наполняется содержимым
        // файла srcPath
        void CreateFile(string srcPath, string path);

        void DeleteFile(string path);

        void UpdateFile(string path);

        // Перегруженная версия предыдущего метода:
        // версия файла обновляется, а сам файл заполняется
        // содержимым файла srcPath
        void UpdateFile(string srcPath, string path);

        string GetFile(string path);

        void CopyContent(string srcPath, string destPath);
    }
}