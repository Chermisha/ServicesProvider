using ServicesProvider.Domain.Models;

namespace ServicesProvider.Application.Services
{
    public interface IServicesService
    {
        Task AddService(Service service);
        Task<ResponseBase> DeleteService(int id);
        Task<List<Service>> GetAllService();
        Task<Service> GetServiceById(int id);
        Task<ResponseBase<Service>> UpdateService(int id, Service service);
    }
}