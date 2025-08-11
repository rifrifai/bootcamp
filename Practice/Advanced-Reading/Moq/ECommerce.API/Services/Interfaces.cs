using ECommerce.API.Models;

namespace ECommerce.API.Service;

public interface ICartService
{
  string ValidateCart(Order order);
}

public interface IPaymentService
{
  string ChargeAndShip(Order order);
  bool MakePayment(Card card);
}

public interface IShipmentService
{
  ShipmentDetails? Ship(AddressInfo address);
}