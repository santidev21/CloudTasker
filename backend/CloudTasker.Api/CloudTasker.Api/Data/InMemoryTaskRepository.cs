using CloudTasker.Api.Models;
using System.Collections.Concurrent;

namespace CloudTasker.Api.Data
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly ConcurrentDictionary<string, TaskItem> _store = new();

        public InMemoryTaskRepository()
        {
            var now = DateTimeOffset.UtcNow;
            var t1 = new TaskItem(Guid.NewGuid().ToString("n"), "First task", "Demo item", null, false, now, now);
            var t2 = new TaskItem(Guid.NewGuid().ToString("n"), "Buy coffee", null, now.AddDays(1), false, now, now);
            _store.TryAdd(t1.Id, t1);
            _store.TryAdd(t2.Id, t2);
        }

        public Task<IEnumerable<TaskItem>> ListAsync(CancellationToken ct = default)
            => Task.FromResult<IEnumerable<TaskItem>>(_store.Values.OrderBy(x => x.CreatedAt));

        public Task<TaskItem?> GetAsync(string id, CancellationToken ct = default)
            => Task.FromResult(_store.TryGetValue(id, out var item) ? item : null);

        public Task<TaskItem> CreateAsync(TaskItem item, CancellationToken ct = default)
        {
            _store[item.Id] = item;
            return Task.FromResult(item);
        }

        public Task<bool> UpdateAsync(TaskItem item, CancellationToken ct = default)
        {
            if (!_store.ContainsKey(item.Id)) return Task.FromResult(false);
            _store[item.Id] = item;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
            => Task.FromResult(_store.TryRemove(id, out _));
    }
}
