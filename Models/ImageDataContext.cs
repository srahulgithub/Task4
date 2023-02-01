using MongoDB.Driver;

public class ImageDataContext
{
    private readonly IMongoDatabase _database;

    public ImageDataContext(IMongoClient client)
    {
        _database = client.GetDatabase("ImageData");
    }

    public IMongoCollection<ImageData> ImageData => _database.GetCollection<ImageData>("ImageData");
}
