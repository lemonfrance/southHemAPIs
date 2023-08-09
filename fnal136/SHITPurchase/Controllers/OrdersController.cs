using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SHITPurchase.Models;
using SHITPurchase.Data;
using SHITPurchase.Dtos;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SHITPurchase.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IWebAPIRepo _repository;
        public OrdersController(IWebAPIRepo repository)
        {
            _repository = repository;
        }

        //[HttpGet("GetRecentOrder")]
        public ActionResult<OrderOutputDto> GetRecentOrder()
        {
            Order recentorder = _repository.GetLastOrder();
            OrderOutputDto ro = new OrderOutputDto
            {
                Id = recentorder.Id,
                UserName = recentorder.UserName,
                ProductID = recentorder.ProductID,
                Quantity = recentorder.Quantity
            };

            return Ok(ro);
        }

        [Authorize(AuthenticationSchemes="MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpPost("PurchaseItem")]
        public ActionResult<OrderOutputDto> PurchaseItem(OrderInputDto orderInput)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("username");
            string username = c.Value;
            //

            Order order = new Order
            {
                UserName = username,
                ProductID = orderInput.ProductID,
                Quantity = orderInput.Quantity
            };
            Order newOrder = _repository.AddOrder(order);
            OrderOutputDto ord = new OrderOutputDto
            {
                Id = newOrder.Id,
                UserName = newOrder.UserName,
                ProductID = newOrder.ProductID,
                Quantity = newOrder.Quantity
            }; 
            return CreatedAtAction(nameof(GetRecentOrder), new { id = ord.Id }, ord);
        }

        [Authorize(AuthenticationSchemes = "MyAuthentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("PurchaseSingleItem/{productid}")]
        public ActionResult<OrderOutputDto> PurchaseSingleItem(int productid)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("username");
            string username = c.Value;
            //

            Product item = _repository.GetProductByID(productid);
            if (item == null)
                return NotFound();
            else
            {
                Order order = new Order
                {
                    UserName = username,
                    ProductID = productid,
                    Quantity = 1
                };
                Order newOrder = _repository.AddOrder(order);
                OrderOutputDto ord = new OrderOutputDto
                {
                    Id = newOrder.Id,
                    UserName = newOrder.UserName,
                    ProductID = newOrder.ProductID,
                    Quantity = newOrder.Quantity
                };
                return CreatedAtAction(nameof(GetRecentOrder), new { id = ord.Id }, ord);
            }
        }
    }
}