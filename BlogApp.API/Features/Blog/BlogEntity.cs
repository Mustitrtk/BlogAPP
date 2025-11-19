using MongoDB.Bson.Serialization.Attributes;

namespace BlogApp.API.Features.Blog
{
    public class BlogEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }

        public Guid CategoryId { get; set; }
        public BlogApp.API.Features.Category.Category Category { get; set; }
    }
}
