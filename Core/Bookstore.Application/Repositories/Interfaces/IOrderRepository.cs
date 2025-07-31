using Bookstore.Application.DTOs.BookOrder;
using Bookstore.Application.Handlers.Orders.Commands.Create;
using Bookstore.Application.Handlers.Orders.Queries.GetAll;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Responses;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order, CreateOrderCommand, OrderResponse>
    {
        Task<bool> AddBookOrder(IEnumerable<CreateBookOrderDTO> dto, CancellationToken cancellationToken);
        Task<ICollection<OrderResponse>> GetAllByFilter(GetAllOrderQuery query, Func<Order, OrderResponse> map, CancellationToken cancellationToken);
    }
}
