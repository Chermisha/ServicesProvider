using Microsoft.EntityFrameworkCore;
using ServicesProvider.Domain.Models;
using ServicesProvider.Persistence;
using ServicesProvider.Persistence.Entities;
using System.Xml.Linq;

namespace ServicesProvider.Application.Services
{
    public class ServiceCategoriesService : IServiceCategoriesService
    {
        private readonly ApplicationDbContext _dbContext;

        public ServiceCategoriesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ServiceCategory>> GetAllServiceCategories()
        {
            var serviceCategoryEntity = await _dbContext.ServiceCategories
                .AsNoTracking()
                .ToListAsync();

            var serviceCategories = serviceCategoryEntity
                .Select(sc => new ServiceCategory
                {
                    Id = sc.Id,
                    Name = sc.Name
                }).ToList();

            return serviceCategories;
        }

        public async Task<ResponseBase<ServiceCategory>> GetServiceCategoryById(int id)
        {
            var serviceCategoryEntity = await _dbContext.ServiceCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (serviceCategoryEntity == null)
            {
                return new ResponseBase<ServiceCategory>(1, "Услуга не найдена");
            }
            else
            {
                var serviceCategory = new ServiceCategory
                {
                    Id = serviceCategoryEntity.Id,
                    Name = serviceCategoryEntity.Name
                };
                return new ResponseBase<ServiceCategory>(0, "Успех", serviceCategory);
            }
        }

        public async Task AddServiceCategory(string name)
        {
            var serviceCategoryEntity = new ServiceCategoryEntity
            {
                Name = name
            };

            await _dbContext.ServiceCategories.AddAsync(serviceCategoryEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ResponseBase<ServiceCategory>> UpdateServiceCategory(int id, string name)
        {
            var entityToUpdate = await _dbContext.ServiceCategories
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (entityToUpdate == null)
            {
                return new ResponseBase<ServiceCategory>(1, "Категория не найдена");
            }

            entityToUpdate.Name = name;

            await _dbContext.SaveChangesAsync();

            var updatedServiceCategory = new ServiceCategory
            {
                Id = entityToUpdate.Id,
                Name = entityToUpdate.Name
            };

            return new ResponseBase<ServiceCategory>(0, "Успешно", updatedServiceCategory);
        }

        public async Task<ResponseBase> DeleteServiceCategory(int id)
        {
            var serviceCategoryToDelete = await _dbContext.ServiceCategories.FindAsync(id);

            if (serviceCategoryToDelete == null)
            {
                return new ResponseBase(1, "Категория не найдена");
            }

            _dbContext.ServiceCategories.Remove(serviceCategoryToDelete);

            await _dbContext.SaveChangesAsync();

            return new ResponseBase(0, "Категория успешно удалена");
        }

    }
}