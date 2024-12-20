using Newtonsoft.Json;
using NotesManagement.Api.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace NotesManagement.Api.Services.Implementations
{
    public class IOService<T> : IIOService<T>
    {
        private readonly ILogger<IOService<T>> _logger;
        private string _filePath;
        private readonly ITokenService _tokenService;

        public IOService(ILogger<IOService<T>> logger, ITokenService tokenService)
        {
            _logger = logger;
            SetFilePath($"Data/{typeof(T).Name}.json");
            _tokenService = tokenService;
        }

        public void SetFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogError("File path cannot be null or empty. UserId: {UserId}", _tokenService.GetUserIdFromToken());
                throw new ArgumentNullException(nameof(filePath));
            }

            _filePath = filePath;

            try
            {
                // Ensure the directory exists
                var directory = Path.GetDirectoryName(_filePath);
                if (directory != null && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    _logger.LogInformation("Directory created at path: {Directory}. UserId: {UserId}", directory, _tokenService.GetUserIdFromToken());
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to create directory for file path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                throw;
            }
        }

        public async Task<IList<T>> ReadDataAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    _logger.LogWarning("File not found at path: {FilePath}. Returning an empty list. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                    return new List<T>();
                }

                var json = await File.ReadAllTextAsync(_filePath);
                _logger.LogInformation("File read successfully from path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());

                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing data from file at path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                return new List<T>();
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "IO error reading file at path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                throw new InvalidOperationException("Failed to read data due to an IO issue.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unexpected error reading file at path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                throw;
            }
        }

        public async Task WriteDataAsync(IList<T> data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                await File.WriteAllTextAsync(_filePath, json);
                _logger.LogInformation("Data written successfully to file at path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error serializing data to file at path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                throw new InvalidOperationException("Failed to serialize data.", ex);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "IO error writing to file at path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                throw new InvalidOperationException("Failed to write data due to an IO issue.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unexpected error writing to file at path: {FilePath}. UserId: {UserId}", _filePath, _tokenService.GetUserIdFromToken());
                throw;
            }
        }
        
    }
}
