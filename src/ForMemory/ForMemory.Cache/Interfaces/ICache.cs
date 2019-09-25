namespace ForMemory.Cache.Interfaces
{
    public interface ICache
    {
        string Get(string key);

        bool Set(string key, string value, int minutes);
    }
}