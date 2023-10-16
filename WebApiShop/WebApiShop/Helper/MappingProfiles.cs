using AutoMapper;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, EmployeeDTO>();
        CreateMap<Product, ProductDTO>();
    }
}