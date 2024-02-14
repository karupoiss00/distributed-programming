namespace Valuator.Storage
{
    public interface IStorage
    {
        public void Save(string key, string value);
        public string Get(string key);
        public List<string> GetAllTexts();
    }
}

