namespace WebApiShop.Models;

public class EmployeeProduct
{
    public int EmployeeId { get; set; }
    public int ProductId { get; set; }
    public Employee Employees { get; set; }
    public Product Products { get; set; }
}