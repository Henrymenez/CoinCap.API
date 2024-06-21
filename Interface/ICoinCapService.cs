using CoinCap.API.Dtos.Response;

namespace CoinCap.API.Interface
{
    public interface ICoinCapService
    {
        Task<Result<ClientResponse>> GetAllCryptoAvailable(string? searchByint, int pageSize, int pageNumber);
    }
}
