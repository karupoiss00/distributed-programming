using StackExchange.Redis;
using Valuator.App.Config;
using Valuator.Storage;

namespace Valuator.Redis
{
    public class RedisStorage : IStorage
    {
        private readonly ILogger<RedisStorage> _logger;
        private readonly IDatabase _db;
        
        public RedisStorage(ILogger<RedisStorage> logger, IConfigProvider configurationProvider)
        {
            _logger = logger;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configurationProvider.GetRedisHost());
            _db = redis.GetDatabase();
        }

        public void Save(string key, string value)
        {
            _db.StringSet(key, value);
        }

        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        public List<string> GetAllTexts()
        {
            var textsKeys = (RedisResult[])_db.Execute("keys", "TEXT-*");
            var texts = new List<string>();
            foreach (RedisResult key in textsKeys)
            {
                texts.Add(Get(key.ToString()));
            }
            
            return texts;
        }
    }
}