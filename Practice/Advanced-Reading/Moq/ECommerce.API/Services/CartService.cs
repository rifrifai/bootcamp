using ECommerce.API.Models;

namespace ECommerce.API.Service;

public class CartService : ICartService
{
  private readonly IPaymentService? _paymentService;

  public CartService(IPaymentService paymentService)
  {
    _paymentService = paymentService;
  }

  public string ValidateCart(Order order)
  {
    if (order.CartItems.Count < 1)
      return "Invalid Card";

    if (order.CartItems.Any(x => x.Quantity < 0 || x.Quantity > 10)) return "Invalid Product Quantity";

    return _paymentService!.ChargeAndShip(order);
  }
}