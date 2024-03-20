using AutoMapper;
using SocialNetwork.Contract.Abstractions.Shared;
using SocialNetwork.Contract.Services.V1.Product;
using SocialNetwork.Domain.Entities;
namespace SocialNetwork.Application.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            //V1
            CreateMap<Product, Response.ProductResponse>().ReverseMap();
            CreateMap<PagedResult<Product>, PagedResult<Response.ProductResponse>>().ReverseMap();

            //V2
            //CreateMap<Product, Response.ProductResponse>().ReserseMap();}

        }
    }
}