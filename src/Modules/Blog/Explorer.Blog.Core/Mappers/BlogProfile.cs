using AutoMapper;
using Explorer.Blog.API.Dtos;
using domainBlog = Explorer.Blog.Core.Domain.Blog;

namespace Explorer.Blog.Core.Mappers;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogDTO, domainBlog>().ReverseMap();
    }
}