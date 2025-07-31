using Bookstore.Application.Handlers.Orders.Commands.Create;
using Bookstore.Application.Handlers.Orders.Commands.Delete;
using Bookstore.Application.Handlers.Orders.Queries.GetAll;
using Bookstore.Application.Handlers.Orders.Queries.GetById;
using Bookstore.Application.Responses;
using Bookstore.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : BaseController
    {
        /// <summary>
        /// Creates the Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Order/Create
        /// {
        ///
        ///     OrderIds: "book IDs (HashSet'Guid')",
        /// }
        /// </remarks>
        /// <param name="command">CreateOrderCommand object</param>
        /// <returns>Returns Guid</returns>
        /// <response code="201">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            Guid response = await Mediator.Send(command);
            return Created("", response);
        }

        /// <summary>
        /// Delete the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /Order/Delete/id
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
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
            DeleteOrderCommand request = new DeleteOrderCommand
            {
                Id = id,
            };
            bool response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Receives a list of all orders by filter
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Order/GetAllByFilter?OrderNumberFrom=1&amp;OrderNumberTo=5&amp;OrderDateFrom=2025-07-31&amp;OrderDateTo=2025-07-31
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
        ///       <td>OrderNumberFrom</td>
        ///       <td>integer</td>
        ///       <td>Minimum order number (inclusive)</td>
        ///     </tr>
        ///     <tr>
        ///       <td>OrderNumberTo</td>
        ///       <td>integer</td>
        ///       <td>Maximum order number (inclusive)</td>
        ///     </tr>
        ///     <tr>
        ///       <td>OrderDateFrom</td>
        ///       <td>date-time</td>
        ///       <td>Start date of order period (inclusive)</td>
        ///     </tr>
        ///     <tr>
        ///       <td>OrderDateTo</td>
        ///       <td>date-time</td>
        ///       <td>End date of order period (inclusive)</td>
        ///     </tr>
        ///   </tbody>
        /// </table>
        /// 
        /// <h3>Usage Notes</h3>
        /// <ul>
        ///   <li>All parameters are optional</li>
        ///   <li>Date format should be ISO 8601 (e.g., YYYY-MM-DD or YYYY-MM-DDTHH:MM:SS)</li>
        ///   <li>If both date filters are provided, OrderDateFrom must be ≤ OrderDateTo</li>
        ///   <li>If both number filters are provided, OrderNumberFrom must be ≤ OrderNumberTo</li>
        /// </ul>
        /// </remarks>
        /// <param name="request">GetAllOrderQuery object</param>
        /// <returns>Returns OrderResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllByFilter([FromQuery] GetAllOrderQuery request)
        {
            ICollection<OrderResponse> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        /// <summary>
        /// Gets the order by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Order/GetById/id
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns OrderResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetByIdOrderQuery request = new GetByIdOrderQuery
            {
                Id = id,
            };
            OrderResponse responce = await Mediator.Send(request);
            return Ok(responce);
        }
    }
}
