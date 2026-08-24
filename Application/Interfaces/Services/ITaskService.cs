using Application.DTOs.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateAsync(CreateTaskRequest request);

        Task<TaskResponse?> GetByIdAsync(Guid id);

        Task<List<TaskResponse>> GetAllAsync();

        Task StartAsync(Guid id);

        Task CompleteAsync(Guid id);
    }
}
