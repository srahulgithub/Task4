using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ImageData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Text { get; set; }
    public ObjectId ImageId { get; set; }
}

