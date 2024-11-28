using MongoDB.Bson.Serialization.Attributes;

namespace UdemyNewMicroservice.Catalog.API.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }


    }
}
