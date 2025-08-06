using Bookstore.Application.DTOs.BookOrder;
using Bookstore.Application.DTOs.Order;
using Bookstore.Application.Handlers.Orders.Commands.Create;
using Bookstore.Application.Handlers.Orders.Queries.GetAll;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order, CreateOrderCommand, OrderGetDTO>
    {
        Task<bool> AddBookOrder(IEnumerable<CreateBookOrderDTO> dto, CancellationToken cancellationToken);
        Task<ICollection<OrderGetDTO>> GetAllByFilter(GetAllOrderQuery query, Func<Order, OrderGetDTO> map, CancellationToken cancellationToken);
    }
}
