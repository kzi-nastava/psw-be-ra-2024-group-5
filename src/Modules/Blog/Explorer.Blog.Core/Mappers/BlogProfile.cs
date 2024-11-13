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
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(dto => new BlogImage(Base64Converter.ConvertToByteArray(dto.Base64Data), dto.ContentType, dto.BlogId))))
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes.Select(dto => new BlogVote(dto.UserId, (VoteType)dto.Type))));

            CreateMap<BlogPost, BlogPostDto>();

            CreateMap<CreateBlogPostDto, BlogPost>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Votes, opt => opt.Ignore());

            CreateMap<BlogPost, CreateBlogPostDto>();

            CreateMap<BlogCommentDto, BlogComment>()
                .ConstructUsing(dto => new BlogComment(dto.BlogId, dto.UserId, dto.CommentText));

            CreateMap<BlogComment, BlogCommentDto>();

            CreateMap<BlogVoteDto, BlogVote>()
                .ConstructUsing(dto => new BlogVote(dto.UserId, (VoteType)dto.Type));

            CreateMap<BlogVote, BlogVoteDto>();

            CreateMap<BlogImageDto, BlogImage>()
                .ConstructUsing(dto => new BlogImage(Base64Converter.ConvertToByteArray(dto.Base64Data), dto.ContentType, dto.BlogId));
            
            CreateMap<BlogImage, BlogImageDto>()
                .ForMember(dest => dest.Base64Data, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Base64Data)));
           
        }
    }
}
