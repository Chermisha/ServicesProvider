using ServicesProvider.Domain.Models;

namespace ServicesProvider.Application.Services
{
    public interface IServicesService
    {
        Task AddService(string name, string? description, decimal price, int categoryId);
        Task<ResponseBase> DeleteService(int id);
        Task<List<Service>> GetAllServices();
        Task<Service> GetServiceById(int id);
        Task<ResponseBase<Service>> UpdateService(int id, string name, string? description, decimal price, int categoryId);
    }
}