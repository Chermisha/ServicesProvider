using Microsoft.EntityFrameworkCore;
using ServicesProvider.Persistence;
using ServicesProvider.Domain.Models;
using ServicesProvider.Persistence.Entities;

namespace ServicesProvider.Application.Services
{
    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext _dbContext;

        public ServicesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Service>> GetAllService()
        {
            var servicesEntity = await _dbContext.Services
                .Include(s=> s.Category)
                .AsNoTracking()
                .ToListAsync();



            var services = servicesEntity
                .Select(s => new Service
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    Category = new ServiceCategory
                    {
                        Id = s.Category.Id,
                        Name = s.Category.Name
                    }
                }).ToList();


            return services;
        }

        public async Task<Service> GetServiceById(int id)
        {
            try
            {

                var serviceEntity = await _dbContext.Services
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == id);

                var service = new Service
                {
                    Id = serviceEntity.Id,
                    Name = serviceEntity.Name,
                    Description = serviceEntity.Description,
                    Price = serviceEntity.Price,
                    Category = new ServiceCategory
                    {
                        Id = serviceEntity.Category.Id,
                        Name = serviceEntity.Category.Name
                    }

                };
                return service;
            }
            catch
            {
                return null;
            }

        }

        public async Task AddService(Service service)
        {
            var serviceEntity = new ServiceEntity
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                Category = new ServiceCategoryEntity
                {
                    Id = service.Category.Id,
                    Name = service.Category.Name
                }
            };

            await _dbContext.Services.AddAsync(serviceEntity);
        }

        public async Task<ResponseBase<Service>> UpdateService(int id, Service service)
        {
            var serviceToUpdate = await _dbContext.Services
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (serviceToUpdate == null)
            {
                return new ResponseBase<Service>(1, "Услуга не найдена");
            }

            serviceToUpdate.Name = service.Name;
            serviceToUpdate.Description = service.Description;
            serviceToUpdate.Price = service.Price;

            if (service.Category != null && service.Category.Id != 0)
            {
                var category = await _dbContext.ServiceCategories.FindAsync(service.Category.Id);
                if (category != null)
                {
                    serviceToUpdate.Category = category;
                }
            }
            else
            {
                serviceToUpdate.Category = null;
            }

            await _dbContext.SaveChangesAsync();

            var updatedService = new Service
            {
                Id = serviceToUpdate.Id,
                Name = serviceToUpdate.Name,
                Description = serviceToUpdate.Description,
                Price = serviceToUpdate.Price,
                Category = new ServiceCategory
                {
                    Id = serviceToUpdate.Id,
                    Name = serviceToUpdate.Name
                }
            };

            return new ResponseBase<Service>(0, "Успешно", updatedService);
        }

        public async Task<ResponseBase> DeleteService(int id)
        {
            var serviceToDelete = await _dbContext.Services.FindAsync(id);

            if (serviceToDelete == null)
            {
                return new ResponseBase(1, "Услуга не найдена");
            }

            _dbContext.Services.Remove(serviceToDelete);

            await _dbContext.SaveChangesAsync();

            return new ResponseBase(0, "Услуга успешно удалена");
        }
    }
}
