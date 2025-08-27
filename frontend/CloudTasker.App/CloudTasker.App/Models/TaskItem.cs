using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudTasker.App.Models
{
    public class TaskItem
    {
        public string Id { get; set; } = default!;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public bool IsDone { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
