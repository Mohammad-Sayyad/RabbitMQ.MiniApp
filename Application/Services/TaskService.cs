using Application.DTOs.Tasks;
using Application.Interfaces.Messaging;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entites;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
   public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMessagePublisher _messagePublisher;

        public TaskService(ITaskRepository taskRepository, IMessagePublisher messagePublisher)
        {
            _taskRepository = taskRepository;
            _messagePublisher = messagePublisher;
        }

        public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Task title is requierd");

            var task = new TaskItem(request.Title , request.Description);
            
            await _taskRepository.AddAsync(task);

            var TaskCreatedEvent = new TaskCreatedEvent
            {
                TaskId = task.Id,
                Title = task.Title,
                CreatedAt = task.CreatedAt
            };


            await _messagePublisher.PublishAsync(TaskCreatedEvent, "task.exChange" , "task.created");

            return MapToResponse(task);
        }


        public async Task<TaskResponse?> GetByIdAsync(Guid id)
        {
            var tasks = await _taskRepository.GetByIdAsync(id);

            return tasks is null ? null : MapToResponse(tasks);
        }

        public async Task<List<TaskResponse>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks.Select(MapToResponse).ToList();
        }

        public async Task StartAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task is null)
            {
                throw new ArgumentException("not found");
            }
                task.Start();

                await _taskRepository.AddAsync(task);
            
        }
        public async Task CompleteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if(task is null)
            {
                throw new ArgumentException("Task not found");
            }

            task.Complete();
            await _taskRepository.UpdateAsync(task);
        }


        private static TaskResponse MapToResponse(TaskItem task) {

            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                Status = task.Status,
            };
        
        }
    }
}
