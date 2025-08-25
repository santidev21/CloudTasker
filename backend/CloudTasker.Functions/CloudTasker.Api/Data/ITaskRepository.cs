using CloudTasker.Api.Models;

namespace CloudTasker.Api.Data
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> ListAsync(CancellationToken ct = default);
        Task<TaskItem?> GetAsync(string id, CancellationToken ct = default);
        Task<TaskItem> CreateAsync(TaskItem item, CancellationToken ct = default);
        Task<bool> UpdateAsync(TaskItem item, CancellationToken ct = default);
        Task<bool> DeleteAsync(string id, CancellationToken ct = default);
    }
}
