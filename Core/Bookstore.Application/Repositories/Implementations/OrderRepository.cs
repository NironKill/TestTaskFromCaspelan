using Bookstore.Application.DTOs.BookOrder;
using Bookstore.Application.DTOs.Order;
using Bookstore.Application.Handlers.Orders.Commands.Create;
using Bookstore.Application.Handlers.Orders.Queries.GetAll;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Repositories.Implementations
{
    public class OrderRepository : BaseRepository<Order, CreateOrderCommand, OrderGetDTO>, IOrderRepository
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

        public async Task<ICollection<OrderGetDTO>> GetAllByFilter(GetAllOrderQuery query, Func<Order, OrderGetDTO> map, CancellationToken cancellationToken)
        {
            IQueryable<Order> orderQuery = _context.Orders;

            if (query.OrderNumberFrom is not null)
                orderQuery = orderQuery.Where(x => x.Number >= query.OrderNumberFrom);
            if (query.OrderNumberTo is not null)
                orderQuery = orderQuery.Where(x => x.Number <= query.OrderNumberTo);

            if (query.OrderDateFrom is not null)
                orderQuery = orderQuery.Where(x => x.OrderedIn >= query.OrderDateFrom);
            if (query.OrderDateTo is not null)
                orderQuery = orderQuery.Where(x => x.OrderedIn <= query.OrderDateTo);

            List<Order> orders = await orderQuery.ToListAsync(cancellationToken);
            return orders.Select(map).ToList();
        }
    }
}
