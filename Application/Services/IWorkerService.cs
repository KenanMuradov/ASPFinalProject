using Application.Models.DTOs;
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
        Task<bool> AcceptWorkAsync(string id);
    }
}
