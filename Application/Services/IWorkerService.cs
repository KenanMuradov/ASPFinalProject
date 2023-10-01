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
        Task<ProfileDTO> GetWorkerProfile(string email);
        Task<bool> AcceptWorkAsync(AcceptWorkRequest request);
        Task<bool> SetWorkDoneAsync(SetWorkDoneRequest requestId);
        IEnumerable<RequestDTO> SeeInactiveRequests(string email);
        IEnumerable<RequestDTO> SeeActiveRequests(string email);
        IEnumerable<RequestDTO> SeeCompletedTasks(string email);
    }
}
