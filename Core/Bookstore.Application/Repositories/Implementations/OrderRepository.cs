using Bookstore.Application.DTOs.BookOrder;
using Bookstore.Application.Handlers.Orders.Commands.Create;
using Bookstore.Application.Handlers.Orders.Queries.GetAll;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Application.Responses;
using Bookstore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Repositories.Implementations
{
    public class OrderRepository : BaseRepository<Order, CreateOrderCommand, OrderResponse>, IOrderRepository
    {
        public OrderRepository(IApplicationDbContext context) : base(context) { }

        public async Task<bool> AddBookOrder(IEnumerable<CreateBookOrderDTO> dtos, CancellationToken cancellationToken)
        {
            IEnumerable<BookOrder> newBookOrders = dtos.Select(dto => new BookOrder()
            {
                Id = Guid.NewGuid(),
                BookId = dto.BookId,
                OrderId = dto.OrderId,
                Price = dto.Price
            });

            await _context.BookOrders.AddRangeAsync(newBookOrders, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ICollection<OrderResponse>> GetAllByFilter(GetAllOrderQuery query, Func<Order, OrderResponse> map, CancellationToken cancellationToken)
        {
            IQueryable<Order> orderQuery = _context.Orders.AsQueryable();

            if (query.OrderNumberFrom is not null)
                orderQuery = orderQuery.Where(x => x.Number >= query.OrderNumberFrom).AsQueryable();
            if (query.OrderNumberTo is not null)
                orderQuery = orderQuery.Where(x => x.Number <= query.OrderNumberTo).AsQueryable();

            if (query.OrderDateFrom is not null)
                orderQuery = orderQuery.Where(x => x.OrderedIn >= query.OrderDateFrom).AsQueryable();
            if (query.OrderDateTo is not null)
                orderQuery = orderQuery.Where(x => x.OrderedIn >= query.OrderDateTo).AsQueryable();

            List<Order> orders = await orderQuery.ToListAsync();
            return orders.Select(map).ToList();
        }
    }
}
