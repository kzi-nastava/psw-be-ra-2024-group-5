using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Utilities;

namespace Explorer.Blog.Core.Mappers
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogPostDto, BlogPost>()
                .IncludeAllDerived()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.images.Select(dto => new BlogImage(Base64Converter.ConvertToByteArray(dto.base64Data), dto.contentType, dto.blogId))))
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.votes.Select(dto => new BlogVote(dto.userId, (VoteType)dto.type))));

            CreateMap<BlogPost, BlogPostDto>();

            CreateMap<CreateBlogPostDto, BlogPost>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Votes, opt => opt.Ignore());

            CreateMap<BlogPost, CreateBlogPostDto>();

            CreateMap<BlogCommentDto, BlogComment>()
                .ConstructUsing(dto => new BlogComment(dto.blogId, dto.userId, dto.commentText));

            CreateMap<BlogComment, BlogCommentDto>();

            CreateMap<BlogVoteDto, BlogVote>()
                .ConstructUsing(dto => new BlogVote(dto.userId, (VoteType)dto.type));

            CreateMap<BlogVote, BlogVoteDto>();

            CreateMap<BlogImageDto, BlogImage>()
                .ConstructUsing(dto => new BlogImage(Base64Converter.ConvertToByteArray(dto.base64Data), dto.contentType, dto.blogId));
            
            CreateMap<BlogImage, BlogImageDto>()
                .ForMember(dest => dest.base64Data, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Base64Data)));
           
        }
    }
}
