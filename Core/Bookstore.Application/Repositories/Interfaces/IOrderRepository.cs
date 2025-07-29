using Bookstore.Application.DTOs.Order.Commands;
using Bookstore.Application.DTOs.Order.Queries;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order, OrderCreateDTO, OrderGetDTO>
    {
    }
}
