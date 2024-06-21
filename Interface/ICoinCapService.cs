using CoinCap.API.Dtos.Response;

namespace CoinCap.API.Interface
{
    public interface ICoinCapService
    {
        Task<Result<CoinCapBaseResponse>> GetAllCryptoAvailable(string? searchBy);
    }
}
