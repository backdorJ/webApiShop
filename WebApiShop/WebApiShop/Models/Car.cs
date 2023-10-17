namespace WebApiShop.Models;

public class Car
{
    public int Id { get; set; }
    public string CarName { get; set; }
    public string Color { get; set; }
    public Country Country { get; set; }
}