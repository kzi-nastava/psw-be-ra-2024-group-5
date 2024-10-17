using Explorer.Blog.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IBlogCommentService
    {
        Result<BlogCommentDTO> CreateComment(BlogCommentDTO commentDto);
        Result<BlogCommentDTO> GetCommentById(int id);
        Result<BlogCommentDTO> UpdateComment( BlogCommentDTO commentDto);
        Result<bool> DeleteComment(long id);

        // Metoda koja vraća sve komentare od određenog korisnika
      //  Result<IEnumerable<BlogCommentDTO>> GetCommentsByUserId(int userId);
    }
}
