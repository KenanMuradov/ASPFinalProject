using Application.Models.DTOs.Worker;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly UserManager<User> _userManager;
        private readonly IWorkRequestRepository _workRequestRepository;
        private readonly IWorkerCategoryRepository _workerCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMailService _mailService;

        public WorkerService(UserManager<User> userManager, IWorkRequestRepository workRequestRepository, IMailService mailService, ICategoryRepository categoryRepository, IWorkerCategoryRepository workerCategoryRepository)
        {
            _userManager = userManager;
            _workRequestRepository = workRequestRepository;
            _mailService = mailService;
            _categoryRepository = categoryRepository;
            _workerCategoryRepository = workerCategoryRepository;
        }

        public async Task<bool> AcceptWorkAsync(AcceptWorkRequest request)
        {
            try
            {
                var worker = await _userManager.FindByEmailAsync(request.WorkerEmail);
                if (worker is not null)
                {
                    var workRequest = await _workRequestRepository.GetAsync(request.TaskId);
                    if (workRequest is null || workRequest.WorkerEmail != worker.Email)
                        return false;

                    workRequest.IsAccepted = true;
                    _workRequestRepository.Update(workRequest);
                    await _workRequestRepository.SaveChangesAsync();
                    _mailService.SendTaskAcceptanceMessage(workRequest.ClientEmail, workRequest.WorkerEmail);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProfileDTO?> GetWorkerProfile(string email)
        {
            try
            {
                var worker = await _userManager.FindByEmailAsync(email);
                if (worker is not null)
                {
                    var requests = _workRequestRepository.GetWhere(r => r.WorkerEmail == worker.Email).ToList();
                    var totalRequests = requests.Count;
                    var inactiveRequests = requests.Where(r => !r.IsAccepted.HasValue).Count();
                    var activeRequests = requests.Where(r => r.IsAccepted.HasValue && r.IsAccepted.Value && !r.IsCompleted).Count();
                    var completedRequests = requests.Where(r => r.IsCompleted).Count();
                    var rating = _workRequestRepository.GetWhere(r => r.WorkerEmail == worker.Email && r.Rating.HasValue).Select(r => r.Rating.Value).Sum();
                    if (_workRequestRepository.GetWhere(r => r.WorkerEmail == worker.Email && r.Rating.HasValue).Any())
                        rating /= _workRequestRepository.GetWhere(r => r.WorkerEmail == worker.Email && r.Rating.HasValue).Count();
                    else
                        rating = 0;


                    var dto = new ProfileDTO
                    {
                        FirstName = worker.FirstName,
                        LastName = worker.LastName,
                        Email = worker.Email,
                        ActiveRequestsCount = activeRequests,
                        CompletedRequestsCount = completedRequests,
                        InactiveRequestsCount = inactiveRequests,
                        TotalRequestsCount = totalRequests,
                        Rating = rating
                    };
                    return dto;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RegisterInNewCategory(CategoryRegisterRequest request)
        {
            var worker = await _userManager.FindByEmailAsync(request.WorkerEmail);
            if (worker is not null)
            {
                var category = await _categoryRepository.GetAsync(request.CategoryId);
                if (category is null)
                    return false;

                var workerCategory = new WorkerCategory { CategoryId = category.Id, UserId = worker.Id };


                await _workerCategoryRepository.AddAsync(workerCategory);
                await _workerCategoryRepository.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> RejectWorkAsync(RejectWorkRequest request)
        {
            var worker = await _userManager.FindByEmailAsync(request.WorkerEmail);
            if (worker is not null)
            {
                var workRequest = await _workRequestRepository.GetAsync(request.TaskId);
                if (workRequest is null || workRequest.WorkerEmail != worker.Email)
                    return false;

                workRequest.IsAccepted = false;
                _workRequestRepository.Update(workRequest);
                await _workRequestRepository.SaveChangesAsync();
                _mailService.SendTaskRejectionMessage(workRequest.ClientEmail);
                return true;
            }
            return false;
        }

        public IEnumerable<RequestDTO> SeeActiveRequests(string email)
            => _workRequestRepository
            .GetWhere(r => r.WorkerEmail == email && r.IsAccepted.HasValue && r.IsAccepted.Value)
            .Select(r => new RequestDTO { Id = r.Id.ToString(), Message = r.Message, UserMail = r.ClientEmail });

        public IEnumerable<RequestDTO> SeeCompletedTasks(string email)
            => _workRequestRepository
                .GetWhere(r => r.WorkerEmail == email || r.IsCompleted)
                .Select(r => new RequestDTO { Id = r.Id.ToString(), Message = r.Message, UserMail = r.ClientEmail });

        public IEnumerable<RequestDTO> SeeInactiveRequests(string email)
            => _workRequestRepository
                .GetWhere(r => r.WorkerEmail == email && !r.IsAccepted.HasValue)
                .Select(r => new RequestDTO { Id = r.Id.ToString(), Message = r.Message, UserMail = r.ClientEmail });

        public async Task<bool> SetWorkDoneAsync(SetWorkDoneRequest request)
        {
            try
            {

            }
            catch (Exception)
            {
                return false;
            }
            var worker = await _userManager.FindByEmailAsync(request.WorkerEmail);
            if (worker is not null)
            {
                var workRequest = await _workRequestRepository.GetAsync(request.TaskId);
                if (workRequest is null || workRequest.WorkerEmail != worker.Email || !workRequest.IsAccepted.Value)
                    return false;

                workRequest.IsCompleted = true;
                _workRequestRepository.Update(workRequest);
                await _workRequestRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
