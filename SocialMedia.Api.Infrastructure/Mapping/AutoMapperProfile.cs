using AutoMapper;
using SocialMedia.Api.Core.DTOs;
using SocialMedia.Api.Core.Entities;

namespace SocialMedia.Api.Infrastructure.Mapping
{
    public  class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Security, SecurityDto>().ReverseMap();
        }
    }
}
