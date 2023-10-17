using System.ComponentModel.DataAnnotations;

namespace WebApiShop.Models;

public class Peet
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}