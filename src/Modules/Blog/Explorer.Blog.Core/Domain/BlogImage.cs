using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogImage : ValueObject
    {
        public byte[] base64Data { get; private set; }  
        public string contentType { get; private set; }
        public int blogId { get; private set; }


        public BlogImage() { }

        [JsonConstructor]
        public BlogImage(byte[] base64Data, string contentType, int blogId)
        {
            this.base64Data = base64Data ?? throw new ArgumentNullException(nameof(base64Data));
            this.contentType = !string.IsNullOrWhiteSpace(contentType) ? contentType : throw new ArgumentNullException(nameof(contentType));
            this.blogId = blogId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return base64Data;
            yield return contentType;
            yield return blogId;
        }
    }
}
