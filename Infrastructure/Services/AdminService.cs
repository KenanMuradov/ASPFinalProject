using Application.Models;
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
    public class AdminService : IAdminService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWorkRequestRepository _workRequestRepository;
        private readonly UserManager<User> _userManager;

        public AdminService(ICategoryRepository categoryRepository, UserManager<User> userManager, IWorkRequestRepository workRequestRepository)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
            _workRequestRepository = workRequestRepository;
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO model)
        {
            try
            {
                var category = new Category { Name = model.Name, Description = model.Description };
                await _categoryRepository.AddAsync(category);
                await _categoryRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddNewAdmin(LoginRequest model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                {
                    user = new User
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                        return false;

                    result = await _userManager.AddToRoleAsync(user, "Admin");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<CategoryShowDTO> GetAllCategories() => _categoryRepository.GetAll().Select(c => new CategoryShowDTO { Id = c.Id.ToString(), Name = c.Name });

        public Statistics GetStatistics()
        {
            var workRequests = _workRequestRepository.GetAll(false);
            var totalRequestsCount = workRequests.Count();
            var yearlyRequestsCount = workRequests.Where(wr=> wr.CreatedTime.Year == DateTime.Now.Year).Count();
            var monthlyRequestsCount = workRequests.Where(wr => wr.CreatedTime.Year == DateTime.Now.Year && wr.CreatedTime.Month == DateTime.Now.Month).Count();
            var dailyRequestsCount = workRequests.Where(wr => wr.CreatedTime == DateTime.Today).Count();
            var acceptedTasks = workRequests.Where(wr => wr.IsAccepted.HasValue && wr.IsAccepted.Value).Count();
            var completedTasks = workRequests.Where(wr => wr.IsCompleted).Count();

            return new Statistics
            {
                TotalWorkRequests = totalRequestsCount,
                ThisYearWorkRequests = yearlyRequestsCount,
                ThisMonthWorkRequests = monthlyRequestsCount,
                TodayWorkRequests = dailyRequestsCount,
                AcceptedWorkRequest = acceptedTasks,
                ComlpetedWorks = completedTasks
            };
        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateDTO model)
        {
            try
            {
                var category = await _categoryRepository.GetAsync(model.Id);

                if (category == null)
                    return false;

                category.Name = model.Name;
                category.Description = model.Description;

                _categoryRepository.Update(category);
                await _categoryRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
