using Microsoft.EntityFrameworkCore;
using ServicesProvider.Data;
using ServicesProvider.Models;

namespace ServicesProvider.Services
{
    public class ServicesService
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
                .Include(s=> s.Contracts)
                .AsNoTracking()
                .ToListAsync();

            var services = servicesEntity
             .Select(r =>
             {
                 new Service
                 {
                     Id = r.Id,
                     Name = r.Name,
                     Description = r.Description,
                     Price = r.Price,
                     Category = r.Category
                     Contracts = r.Contracts 
                 }

             }).ToList();
            return services;
        }
    }
}
