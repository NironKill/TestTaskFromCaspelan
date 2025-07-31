using Bookstore.Application.DTOs.Book;
using Bookstore.Application.Handlers.Books.Commands.Create;
using Bookstore.Application.Handlers.Books.Commands.Delete;
using Bookstore.Application.Handlers.Books.Commands.Patch;
using Bookstore.Application.Handlers.Books.Queries.GetAll;
using Bookstore.Application.Handlers.Books.Queries.GetById;
using Bookstore.Application.Handlers.Orders.Queries.GetAll;
using Bookstore.Application.Responses;
using Bookstore.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : BaseController
    {
        /// <summary>
        /// Adds a book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Book/Create
        /// {
        ///
        ///     Price: "book price (decimal)",
        ///     Title: "book title (string)",
        ///     ISBN: "ISBN of the book (string)",
        ///     Author: "author of the book (string)",
        ///     Publisher: "publisher of the book (string)",
        ///     ImageUrl: "link to the book cover (string)"
        /// }
        /// </remarks>
        /// <param name="command">CreateBookCommand object</param>
        /// <returns>Returns Guid</returns>
        /// <response code="201">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
        {
            Guid response = await Mediator.Send(command);
            return Created("", response);
        }

        /// <summary>
        /// Delete the book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /Book/Delete/id
        /// </remarks>
        /// <param name="id">Book id (guid)</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteBookCommand request = new DeleteBookCommand
            {
                Id = id,
            };
            bool response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Change the price of the book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /Book/Patch/id
        /// {
        ///
        ///     Price: "book price (decimal)",
        /// }
        /// </remarks>
        /// <param name="id">Book id (guid)</param>
        /// <returns>Returns BookResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(Guid id, [FromBody] BookUpdateDTO dto)
        {
            PatchBookCommand request = new PatchBookCommand
            {
                Id = id,
                Price = dto.Price
            };

            BookResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Gets a list of all books by filter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Book/GetAllByFilter?Title=A Song of Ice and Fire&amp;PublicationDateFrom=2025-07-31&amp;PublicationDateTo=2025-07-31
        ///
        /// <h3>Filter Parameters</h3>
        /// <table>
        ///   <thead>
        ///     <tr>
        ///       <th>Parameter</th>
        ///       <th>Type</th>
        ///       <th>Description</th>
        ///     </tr>
        ///   </thead>
        ///   <tbody>   
        ///     <tr>
        ///       <td>Title</td>
        ///       <td>string</td>
        ///       <td>Search by starts with of book titles</td>
        ///     </tr>
        ///     <tr>
        ///       <td>PublicationDateFrom</td>
        ///       <td>date-time</td>
        ///       <td>Search for books from this publication date (inclusive)</td>
        ///     </tr>
        ///     <tr>
        ///       <td>PublicationDateTo</td>
        ///       <td>date-time</td>
        ///       <td>Search for books published before this date (inclusive)</td>
        ///     </tr>
        ///   </tbody>
        /// </table>
        /// 
        /// <h3>Usage Notes</h3>
        /// <ul>
        ///   <li>All parameters are optional</li>
        ///   <li>Date format should be ISO 8601 (e.g., YYYY-MM-DD or YYYY-MM-DDTHH:MM:SS)</li>
        ///   <li>If both date filters are provided, PublicationDateFrom must be ≤ PublicationDateTo</li>
        /// </ul>
        /// </remarks>
        /// <param name="request">GetAllBookQuery object</param>
        /// <returns>Returns BookResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllByFilter([FromQuery] GetAllBookQuery request)
        {
            ICollection<BookResponse> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        /// <summary>
        /// Gets the book by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Book/GetById/id
        /// </remarks>
        /// <param name="id">Book id (guid)</param>
        /// <returns>Returns BookResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetByIdBookQuery request = new GetByIdBookQuery
            {
                Id = id,
            };
            BookResponse responce = await Mediator.Send(request);
            return Ok(responce);
        }
    }
}