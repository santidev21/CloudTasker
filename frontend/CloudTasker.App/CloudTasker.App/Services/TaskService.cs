using System.Net.Http.Json;
using CloudTasker.App.Models;

namespace CloudTasker.App.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://cloudtaskerfunc-santi-eqeqa2ghgxgqcfar.canadacentral-01.azurewebsites.net/api";

        public TaskService(HttpClient? client = null)
        {
            _httpClient = client ?? new HttpClient();
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TaskItem>>($"{BaseUrl}/tasks");
            return response ?? new List<TaskItem>();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<TaskItem>($"{BaseUrl}/tasks/{id}");
        }

        public async Task<TaskItem?> CreateTaskAsync(TaskItem task)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/tasks", task);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<TaskItem>();
        }

        public async Task<TaskItem?> UpdateTaskAsync(string id, TaskItem task)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/tasks/{id}", task);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<TaskItem>();
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/tasks/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
