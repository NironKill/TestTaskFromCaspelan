using Bookstore.Application.DTOs.Order.Commands;
using Bookstore.Application.DTOs.Order.Queries;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Implementations
{
    public class OrderRepository : BaseRepository<Order, OrderCreateDTO, OrderGetDTO>, IOrderRepository
    {
        public OrderRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
