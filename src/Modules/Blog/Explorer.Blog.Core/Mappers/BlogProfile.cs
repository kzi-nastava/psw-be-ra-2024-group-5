using AutoMapper;
using Explorer.Blog.API.Dtos;
using domainBlog = Explorer.Blog.Core.Domain.BlogPost;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogDTO, domainBlog>().ReverseMap();
    }
}