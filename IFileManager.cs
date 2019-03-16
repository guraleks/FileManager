namespace FileManager
{
    // Для реализации другого типа хранения данных
    // необходимо имплементировать этот интерфейс
    public interface IFileManager
    {
        void CreateFile (string path);

        void DeleteFile(string path);

        void UpdateFile(string path);

        string GetFile(string path);
    }
}