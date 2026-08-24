using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entites
{

    public class TaskItem
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; } = null!;

        public string? Description { get; private set; }

        public TaskItemStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private TaskItem()
        {
        }

        public TaskItem(string title, string? description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = TaskItemStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void Start()
        {
            Status = TaskItemStatus.InProgress;
        }

        public void Complete()
        {
            Status = TaskItemStatus.Completed;
        }
    }
}
