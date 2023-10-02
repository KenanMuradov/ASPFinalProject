using Application.Models.DTOs;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class WorkerCategoryService : IWorkerCategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWorkerCategoryRepository _workerCategoryRepository;

        public WorkerCategoryService(IWorkerCategoryRepository workerCategoryRepository, ICategoryRepository categoryRepository)
        {
            _workerCategoryRepository = workerCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> AddAsync(WorkerCategoryDTO model)
        {
            if(await _categoryRepository.GetAsync(model.CategoryId) is null) 
                return false;

            var entity = new WorkerCategory()
            {
                CategoryId = Guid.Parse(model.CategoryId),
                UserId = model.WorkerId
            };

            var result = await _workerCategoryRepository.AddAsync(entity);
            await _workerCategoryRepository.SaveChangesAsync();
            return result;
        }
    }
}
