namespace ECommerce.API.Models;

public class Order
{
  public List<CartItem> CartItems { get; set; } = new();
  public Card Card { get; set; } = new();
  public AddressInfo AddressInfo { get; set; } = new();
}


public class CartItem
{
  public string ProductId { get; set; } = string.Empty;
  public int Quantity { get; set; }
  public decimal Price { get; set; }
  public string ProductName { get; set; } = string.Empty;
}

public class Card
{
  public string CardNumber { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public DateTime ValidTo { get; set; }
  public double Amount { get; set; }
  public string CVV { get; set; } = string.Empty;
}

public class AddressInfo
{
  public string Street { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string ZipCode { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
}

public class ShipmentDetails
{
  public string TrackingNumbers { get; set; } = string.Empty;
  public DateTime EstimatedDelevery { get; set; }
  public string Carrier { get; set; } = string.Empty;
  public string Status { get; set; } = "Processing";
}