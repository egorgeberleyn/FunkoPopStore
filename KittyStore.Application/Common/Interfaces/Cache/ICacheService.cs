namespace KittyStore.Application.Common.Interfaces.Cache
{
    public interface ICacheService
    {
        Task<T> GetDataAsync<T>(string key);
    
        Task SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime);
    
        Task<bool> RemoveDataAsync(string key);
    }
}