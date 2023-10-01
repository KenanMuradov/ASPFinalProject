using Application.Models.DTOs;
using Application.Models.DTOs.Category;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly UserManager<User> _userManager;
        private readonly IWorkRequestRepository _workRequestRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWorkerCategoryRepository _workerCategoryRepository;

        public ClientService(UserManager<User> userManager, IWorkRequestRepository workRequestRepository, IWorkerCategoryRepository workerCategoryRepository, ICategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _workRequestRepository = workRequestRepository;
            _workerCategoryRepository = workerCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<WorkerDTO>> GetAllWorkersAsync()
        {
            var workers = await _userManager.GetUsersInRoleAsync("Worker");

            var dtos = workers.Select(w => new WorkerDTO { Email = w.Email, Rating = CalculateRating(w) });

            return dtos;
        }

        public IEnumerable<WorkerDTO> GetWorkersByCategory(string categoryId)
        {
            return _workerCategoryRepository.GetWhere(cw => cw.CategoryId == Guid.Parse(categoryId)).Select(w => new WorkerDTO { Email = w.Worker.Email, Rating = CalculateRating(w.Worker) });
        }

        public Task<IEnumerable<WorkerDTO>> GetWorkersByRatingAsync(bool desc = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryShowDTO> SeeAllCategories()
        {
           return _categoryRepository.GetAll().Select(c => new CategoryShowDTO { Id = c.Id.ToString(), Name = c.Name });
        }

        public async Task<bool> SendWorkRequest(WorkRequestDTO request)
        {
            try
            {
                var workRequest = new WorkRequest
                {
                    ClientEmail = request.UserEmail,
                    WorkerEmail = request.WorkerEmail,
                    Message = request.Message,
                    IsCompleted = false
                };

                await _workRequestRepository.AddAsync(workRequest);
                await _workRequestRepository.SaveChangesAsync();

                return true;
            }
            catch 
            {
                return false;
            }
        }


        private double CalculateRating(User worker)
        {
            var totalRatings = _workRequestRepository.GetWhere(r => r.WorkerEmail == worker.Email && r.Rating.HasValue).Select(r => r.Rating.Value).Sum();
            if (totalRatings / _workRequestRepository.GetWhere(r => r.WorkerEmail == worker.Email && r.Rating.HasValue).Count() != 0)
            {
                var avarageRating = totalRatings / _workRequestRepository.GetWhere(r => r.WorkerEmail == worker.Email && r.Rating.HasValue).Count();
                return avarageRating;
            }
            return 0;
        }

    }
}
