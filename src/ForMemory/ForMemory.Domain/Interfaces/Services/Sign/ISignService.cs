namespace ForMemory.Domain.Interfaces.Services.Sign
{
    public interface ISignService
    {
        string SignType { get; }

        bool Next(string signType);

        string Sign(string value);
    }
}