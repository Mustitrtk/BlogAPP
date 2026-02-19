using MongoDB.Bson.Serialization.Attributes;

namespace BlogApp.API.Features.Auth
{
    public class UserEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}
