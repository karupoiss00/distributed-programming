using Valuator.Storage;

namespace Valuator.storage;

public class MockStorage : IStorage
{
    public void Save(string key, string value)
    {
        Console.WriteLine("MockStorage.Save call");
    }

    public string Get(string key)
    {
        return "mocked";
    }

    public List<string> GetAllTexts()
    {
        return ["text", "mock"];
    }
}