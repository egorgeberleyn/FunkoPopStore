using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetUserOrdersAsync(UserId id) =>
        await _context.Orders.Where(ord => ord.UserId == id).ToListAsync();
    
    public async Task CreateOrderAsync(Order newOrder)
    {
        await _context.AddAsync(newOrder);
        await _context.SaveChangesAsync();
    }
}