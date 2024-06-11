using ServicesProvider.Domain.Models;

namespace ServicesProvider.Application.Services
{
    public interface IServiceCategoriesService
    {
        Task AddServiceCategory(string name);
        Task<ResponseBase> DeleteServiceCategory(int id);
        Task<List<ServiceCategory>> GetAllServiceCategories();
        Task<ResponseBase<ServiceCategory>> GetServiceCategoryById(int id);
        Task<ResponseBase<ServiceCategory>> UpdateServiceCategory(int id, string name);
    }
}