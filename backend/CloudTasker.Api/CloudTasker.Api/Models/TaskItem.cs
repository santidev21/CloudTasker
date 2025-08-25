namespace CloudTasker.Api.Models
{
    public record TaskItem(
        string Id,
        string Title,
        string? Description,
        DateTimeOffset? DueDate,
        bool IsDone,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt
    );
}
