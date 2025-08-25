using Newtonsoft.Json;

namespace CloudTasker.Api.Models
{
    public class TaskItem
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public bool IsDone { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public TaskItem(
            string id,
            string title,
            string? description,
            DateTimeOffset? dueDate,
            bool isDone,
            DateTimeOffset createdAt,
            DateTimeOffset updatedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsDone = isDone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public TaskItem() { }
    }
}
