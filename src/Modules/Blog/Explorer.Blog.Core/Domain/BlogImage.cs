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
        public byte[] Base64Data { get; private set; }  
        public string ContentType { get; private set; }
        public int BlogId { get; private set; }


        public BlogImage() { }

        [JsonConstructor]
        public BlogImage(byte[] base64Data, string contentType, int blogId)
        {
            this.Base64Data = base64Data ?? throw new ArgumentNullException(nameof(base64Data));
            this.ContentType = !string.IsNullOrWhiteSpace(contentType) ? contentType : throw new ArgumentNullException(nameof(contentType));
            this.BlogId = blogId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Base64Data;
            yield return ContentType;
            yield return BlogId;
        }
    }
}
