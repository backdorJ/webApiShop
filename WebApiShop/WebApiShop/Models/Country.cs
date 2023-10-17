namespace WebApiShop.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Car> Cars { get; set; }
}