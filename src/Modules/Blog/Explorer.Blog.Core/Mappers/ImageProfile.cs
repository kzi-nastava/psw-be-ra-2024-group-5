using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.Blog.Core.Utilities;

namespace Explorer.Blog.Core.Mappers
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<BlogImageDTO, BlogImage>()
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => Base64Converter.ConvertToByteArray(src.base64Data)))
            .ForMember(dest => dest.contentType, opt => opt.MapFrom(src => src.contentType))
            .ForMember(dest => dest.blogId, opt => opt.MapFrom(src => src.blogId))  
            .ForMember(dest => dest.Id, opt => opt.Ignore());  

            CreateMap<BlogImage, BlogImageDTO>()
                .ForMember(dest => dest.base64Data, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.image)))
                .ForMember(dest => dest.contentType, opt => opt.MapFrom(src => src.contentType))
                .ForMember(dest => dest.blogId, opt => opt.MapFrom(src => src.blogId));  
        }
    }
}
