using System.ComponentModel.DataAnnotations;

namespace UdemyNewMicroservice.Catalog.API.Options
{
    public class MongoOptions
    {
        [Required]
        public string DatabaseName { get; set; } = default!;
        [Required]
        public string ConnectionString { get; set; } = default!;
    }
}
