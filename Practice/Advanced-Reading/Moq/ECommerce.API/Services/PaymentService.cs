using ECommerce.API.Models;

namespace ECommerce.API.Service;

public class PaymentService : IPaymentService
{
  private readonly IShipmentService? _shipmentService;

  public PaymentService(IShipmentService shipmentService)
  {
    _shipmentService = shipmentService;
  }

  public string ChargeAndShip(Order order)
  {
    if (order.Card.Amount <= 0) return "Amount Not Valid";

    if (order.Card == null) return "Payment card is required";

    if (order.Card.ValidTo < DateTime.Now) return "Card Expired";

    if (order.Card.CardNumber.Length < 16) return "CardNumber Not Valid";

    bool paymentSuccess = MakePayment(order.Card);

    if (paymentSuccess)
    {
      var shipment = _shipmentService!.Ship(order.AddressInfo);
      if (shipment != null) return "Item Shipped";
      else return "Something went wrong with the shipment!!!";
    }
    else
    {
      return "Payment Failed";
    }
  }

  public bool MakePayment(Card card)
  {
    return true;
  }
}