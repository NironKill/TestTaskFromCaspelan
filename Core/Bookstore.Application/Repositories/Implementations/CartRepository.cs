using Bookstore.Application.DTOs.Cart.Commands;
using Bookstore.Application.DTOs.Cart.Queries;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Application.Repositories.Interfaces;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Implementations
{
    public class CartRepository : BaseRepository<Cart, CartCreateDTO, CartGetDTO>, ICartRepository
    {
        public CartRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
