using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceProject.Catalog.Api.Repositories;
public class BaseEntity
{
    //snow flake algorithm
    [BsonElement("_id")]
    public Guid Id { get; set; }
}

