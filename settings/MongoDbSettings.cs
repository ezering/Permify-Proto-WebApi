namespace Permify_Proto_WebApi.settings
{
    public class MongoDbSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString
        {
            get
            {
                return $"mongodb://{Username}:{Password}@mic-cluster-shard-00-00.vpzmo.mongodb.net:27017,mic-cluster-shard-00-01.vpzmo.mongodb.net:27017,mic-cluster-shard-00-02.vpzmo.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-4s345a-shard-0&authSource=admin&retryWrites=true&w=majority";
            }
        }
    }
}
