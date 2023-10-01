using Application.Models;
using Application.Models.DTOs;
using Application.Models.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAdminService
    {
        public Task<bool> AddCategoryAsync(CategoryDTO model);
        public Task<bool> UpdateCategoryAsync(CategoryUpdateDTO model);
        public Task<bool> AddNewAdmin(LoginRequest model);
        public IEnumerable<CategoryShowDTO> GetAllCategories();
        public Statistics GetStatistics();
    }
}
