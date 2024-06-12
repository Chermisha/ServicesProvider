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
                    .Include(s => s.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == id);
                if (serviceEntity == null)
                {
                    return null;
                }

                var service = new Service
                {
                    Id = serviceEntity.Id,
                    Name = serviceEntity.Name,
                    Description = serviceEntity.Description ?? string.Empty,
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
                .FirstOrDefaultAsync(sc => sc.Id == categoryId);

            var serviceEntity = new ServiceEntity
            {
                Name = name,
                Description = description ?? string.Empty,
                Price = price,
                Category = categoryEntity
            };

            await _dbContext.Services.AddAsync(serviceEntity);
            await _dbContext.SaveChangesAsync();
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

            bool isNewCategory = entityToUpdate.Category?.Id != categoryId;

            entityToUpdate.Name = name;
            entityToUpdate.Description = description ?? string.Empty;
            entityToUpdate.Price = price;

            if (isNewCategory)
            {
                var categoryEntity = await _dbContext.ServiceCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

                entityToUpdate.Category = categoryEntity;
            }

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
