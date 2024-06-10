using Microsoft.EntityFrameworkCore;
using ServicesProvider.Persistence.Entities;
using ServicesProvider.Persistence;
using ServicesProvider.Domain.Models;
using System.Linq;

namespace ServicesProvider.Application.Services
{
    public class RequestsService : IRequestsService { }
}
    //{
    //    private readonly ApplicationDbContext _context;

    //    public RequestsService(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

        //public async Task<List<Request>> GetAllRequests()
        //{
        //    var requestEntity = await _context.Requests
        //        .AsNoTracking()
        //        .ToListAsync();


    //        var requests = requestEntity
    //         .Select(r =>
    //         {
    //             new Request(r.Id, r.Comment ?? null, r.User, r.Service);

    //         }).ToList();

    //    }
    //    public async Task<Request> GetRequestById(int id)
    //    {
    //        var requestEntity = await _context.Requests
    //            .AsNoTracking()
    //            .FirstOrDefaultAsync(r => r.Id == id);

    //        var request = requestEntity;
    //    }

    //    public async Task<int> CreateRequest(Request request)
    //    {
    //        try
    //        {
    //            var requestEntity = new RequestEntity
    //            {

    //            }

    //        await _context.Requests.AddAsync(requestEntity);
    //            await _context.SaveChangesAsync();
    //            return requestEntity.Id;
    //        }
    //        catch (Exception ex)
    //        {

    //        }
    //    }

    //    public async Task DeleteRequest(RequestEntity requestEntity)
    //    {
    //        _context.Requests.Remove(requestEntity);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task<RequestEntity> UpdateRequest(RequestEntity requestEntity)
    //    {
    //        _context.Requests.Update(requestEntity);
    //        await _context.SaveChangesAsync();
    //        return requestEntity;
    //    }

    //}
//}
