using BlogApp.API.Features.Blog;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogApp.API.Features.Category
{
    public class Category
    {
        [BsonElement("_id")]
        public Guid Id { get; set; } = default!;
        public string Name { get; set; }
        public List<BlogEntity>? Blogs { get; set; }
    }
}
