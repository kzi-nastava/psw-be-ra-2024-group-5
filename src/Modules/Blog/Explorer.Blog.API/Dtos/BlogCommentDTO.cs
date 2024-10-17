using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogCommentDTO
    {
        public int id { get;  set; }
        public int userId { get; set; }
        public string username { get;  set; }
        public string commentText { get;  set; }
        public DateTime creationTime { get;  set; }
        public DateTime? lastEditedTime { get;  set; }
    }
}
