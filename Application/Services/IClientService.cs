using Application.Models.DTOs;
using Application.Models.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IClientService
    {
        Task<IEnumerable<WorkerDTO>> GetAllWorkersAsync();
        Task<IEnumerable<WorkerDTO>> GetWorkersByRatingAsync(bool desc = true);
        IEnumerable<WorkerDTO> GetWorkersByCategory(string categoryId);
        Task<bool> SendWorkRequest(WorkRequestDTO request);
        IEnumerable<CategoryShowDTO> SeeAllCategories();
        IEnumerable<UserRequestDTO> GetUsersRequests(string userEmail);
        Task<bool> RateWorkDoneAsync(RateWorkDTO model);
    }
}
