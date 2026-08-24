using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Tasks
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public TaskItemStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
