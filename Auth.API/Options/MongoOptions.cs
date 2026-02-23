using System.ComponentModel.DataAnnotations;

namespace Auth.API.Options
{
    public class MongoOptions
    {
        [Required]
        public string DatabaseName { get; set; }
        [Required]
        public string ConnectionString { get; set; }
    }
}
