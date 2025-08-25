namespace CloudTasker.Api.Contracts
{
    public class TaskWriteRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public bool? IsDone { get; set; }
    }

    public class TaskResponse
    {
        public string Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public bool IsDone { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
