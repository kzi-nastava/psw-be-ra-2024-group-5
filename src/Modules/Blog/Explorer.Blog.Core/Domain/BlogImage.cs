using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogImage : Entity
    {
        public byte[] image { get; private set; }  
        public string contentType { get; private set; }
        public int blogId { get; private set; }


        public BlogImage() { }
        public BlogImage(byte[] data, string contentType, int blogId)
        {
            this.image = data ?? throw new ArgumentNullException(nameof(data));
            this.contentType = !string.IsNullOrWhiteSpace(contentType) ? contentType : throw new ArgumentNullException(nameof(contentType));
            this.blogId = blogId < 0 ? throw new Exception("invalid blog id") : blogId;
        }
    }
}
