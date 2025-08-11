using ECommerce.API.Models;

namespace ECommerce.API.Service;

public class ShipmentService : IShipmentService
{
  public ShipmentDetails? Ship(AddressInfo address)
  {
    return new ShipmentDetails
    {
      TrackingNumbers = $"TRK{DateTime.Now.Ticks}",
      EstimatedDelevery = DateTime.Now.AddDays(3),
      Carrier = "Demo Express",
      Status = "Processing"
    };
  }
}