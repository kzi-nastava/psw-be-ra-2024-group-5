using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Explorer.Blog.Core.Mappers
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<BlogImage, BlogImageDTO>()
                .ForMember(dest => dest.base64Data, opt => opt.MapFrom(src => Convert.ToBase64String(src.data)))
                .ReverseMap()
                .ForMember(dest => dest.data, opt => opt.MapFrom(src => Convert.FromBase64String(src.base64Data)));
        }
    }
}
