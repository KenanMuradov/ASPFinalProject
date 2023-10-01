using Application.Models.DTOs;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IWorkerCategoryService
    {
        public Task<bool> AddAsync(WorkerCategoryDTO model);
    }
}
