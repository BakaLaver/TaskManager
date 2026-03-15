using EnSoftGroopTest.Models;
using EnSoftGroopTest.Services.Abstraction;
using System.Text.Json;
using System.IO;

namespace EnSoftGroopTest.Services
{
    public class FileService : IFileService
    {
        private readonly string _filePath = "tasks.json";
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<TaskItem>> LoadTasksAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<TaskItem>();

                string json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<List<TaskItem>>(json, _options) ?? new List<TaskItem>();
            }
            catch (Exception ex)
            {
                // Ошибка будет обработана в ViewModel
                throw new Exception($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        public async Task SaveTasksAsync(List<TaskItem> tasks)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, _options);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка сохранения данных: {ex.Message}");
            }
        }
    }
}
