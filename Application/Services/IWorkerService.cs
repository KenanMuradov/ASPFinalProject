using Application.Models.DTOs.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IWorkerService
    {
        Task<ProfileDTO?> GetWorkerProfile(string email);
        Task<bool> AcceptWorkAsync(AcceptWorkRequest request);
        Task<bool> RejectWorkAsync(RejectWorkRequest request);
        Task<bool> SetWorkDoneAsync(SetWorkDoneRequest request);
        Task<bool> RegisterInNewCategory(CategoryRegisterRequest request);
        IEnumerable<RequestDTO> SeeInactiveRequests(string email);
        IEnumerable<RequestDTO> SeeActiveRequests(string email);
        IEnumerable<RequestDTO> SeeCompletedTasks(string email);
    }
}
