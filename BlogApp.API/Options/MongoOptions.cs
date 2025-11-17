using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.Options
{
    public class MongoOptions
    {
        [Required]
        public string DatabaseName { get; set; }
        [Required]
        public string ConnectionString { get; set; }
    }
}
