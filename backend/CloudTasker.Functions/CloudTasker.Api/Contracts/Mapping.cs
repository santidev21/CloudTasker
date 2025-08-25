using CloudTasker.Api.Models;

namespace CloudTasker.Api.Contracts
{
    public static class Mapping
    {
        public static TaskResponse ToResponse(this TaskItem x) => new()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            DueDate = x.DueDate,
            IsDone = x.IsDone,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        };
    }
}
