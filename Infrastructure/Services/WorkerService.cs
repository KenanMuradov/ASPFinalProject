using Application.Models.DTOs;
using Application.Services;

namespace Infrastructure.Services
{
    public class WorkerService : IWorkerService
    {
        public Task<bool> AcceptWorkAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ProfileDTO> GetWorkerProfile(string email)
        {
            throw new NotImplementedException();
        }
    }
}
