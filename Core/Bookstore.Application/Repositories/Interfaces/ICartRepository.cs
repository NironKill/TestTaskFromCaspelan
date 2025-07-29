using Bookstore.Application.DTOs.Cart.Commands;
using Bookstore.Application.DTOs.Cart.Queries;
using Bookstore.Application.Repositories.Abstract;
using Bookstore.Domain.Entity;

namespace Bookstore.Application.Repositories.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart, CartCreateDTO, CartGetDTO>
    {
    }
}
