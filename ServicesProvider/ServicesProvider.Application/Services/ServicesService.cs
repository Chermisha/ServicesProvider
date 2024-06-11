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

        public async Task<List<Service>> GetAllServices()
        {
            var servicesEntity = await _dbContext.Services
                .Include(s => s.Category)
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

        public async Task AddService(string name, string? description, decimal price, int categoryId)
        {
            var categoryEntity = await _dbContext.ServiceCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(sc => sc.Id == categoryId);

            var serviceEntity = new ServiceEntity
            {
                Name = name,
                Description = description,
                Price = price,
                Category = new ServiceCategoryEntity
                {
                    Id = categoryEntity.Id,
                    Name = categoryEntity.Name
                }
            };

            await _dbContext.Services.AddAsync(serviceEntity);
        }

        public async Task<ResponseBase<Service>> UpdateService(int id, string name, string? description, decimal price, int categoryId)
        {
            var entityToUpdate = await _dbContext.Services
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (entityToUpdate == null)
            {
                return new ResponseBase<Service>(1, "Услуга не найдена");
            }

            entityToUpdate.Name = name;
            entityToUpdate.Description = description ?? string.Empty;
            entityToUpdate.Price = price;
            entityToUpdate.CategoryId = categoryId;

            await _dbContext.SaveChangesAsync();

            var updatedService = new Service
            {
                Id = entityToUpdate.Id,
                Name = entityToUpdate.Name,
                Description = entityToUpdate.Description,
                Price = entityToUpdate.Price,
                Category = new ServiceCategory
                {
                    Id = entityToUpdate.Id,
                    Name = entityToUpdate.Name
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
