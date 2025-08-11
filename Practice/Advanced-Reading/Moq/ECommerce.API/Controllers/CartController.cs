using Microsoft.AspNetCore.Mvc;
using ECommerce.API.Models;
using ECommerce.API.Service;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
  private readonly ICartService _cartService;

  public CartController(ICartService cartService)
  {
    _cartService = cartService;
  }

  [HttpPost("checkout")]
  public ActionResult<string> CheckOut(Order order)
  {
    if (order == null) return BadRequest("Order cannot be null");

    try
    {
      var result = _cartService.ValidateCart(order);
      return Ok(result);
    }
    catch (Exception)
    {
      return StatusCode(500, "An error occurred while processing your order");
    }
  }

  [HttpGet("health")]
  public ActionResult<string> Health()
  {
    return Ok("Cart service is healthy");
  }

}