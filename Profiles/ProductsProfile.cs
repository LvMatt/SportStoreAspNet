using AutoMapper;
using SportStore.Dtos;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            //Source -> Target
            CreateMap<Products, ProductsReadDto>();
            CreateMap<ProductsCreateDto, Products>();
            CreateMap<Customers, CustomersReadDto>();
            CreateMap<CustomersRegisterDto, Customers>();

        }
    }
}
