namespace WebApiShop.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public ICollection<EmployeeProduct> ProductsList { get; set; }
}