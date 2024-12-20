namespace NotesManagement.Api.Services.Interfaces
{
    public interface IIOService<T>
    {
        Task<IList<T>> ReadDataAsync();
        Task WriteDataAsync(IList<T> data);
        void SetFilePath(string filePath);
    }

}
