namespace Valuator.App.Config
{
    public class AppConfigProvider : IConfigProvider
    {
        private const string REDIS_HOST_KEY = "REDIS_HOST";

        string IConfigProvider.GetRedisHost()
        {
            var host = System.Environment.GetEnvironmentVariable(REDIS_HOST_KEY);

            if (host == null)
            {
                return "";
            }

            return host;
        }
    }
}