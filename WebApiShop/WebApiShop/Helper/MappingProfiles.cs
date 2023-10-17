using AutoMapper;
using WebApiShop.DTO;
using WebApiShop.Models;

namespace WebApiShop.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, EmployeeDTO>();
        CreateMap<EmployeeDTO, Employee>();
        CreateMap<Product, ProductDTO>();
        CreateMap<Country, CountryDTO>();
        CreateMap<CountryDTO, Country>();
        CreateMap<Car, CarDTO>();
        CreateMap<CarDTO, Car>();
    }
}