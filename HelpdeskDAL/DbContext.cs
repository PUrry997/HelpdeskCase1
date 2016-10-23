using MongoDB.Driver;

namespace HelpdeskDAL
{
    public class DbContext
    {
        public IMongoDatabase Db;

        public DbContext()
        {
            MongoClient client = new MongoClient();
            Db = client.GetDatabase("HelpdeskDB");
        }
        public IMongoCollection<HelpdeskEntity> GetCollection<HelpdeskEntity>()
        {
            return Db.GetCollection<HelpdeskEntity>(typeof(HelpdeskEntity).Name.ToLower() + "s");
        }
    }
}
