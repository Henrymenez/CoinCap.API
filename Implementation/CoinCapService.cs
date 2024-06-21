using CoinCap.API.Dtos.Response;
using CoinCap.API.Interface;

namespace CoinCap.API.Implementation
{
    public class CoinCapService : ICoinCapService
    {
        public CoinCapService()
        {
            
        }

        public async Task<Result<CoinCapBaseResponse>> GetAllCryptoAvailable(string? searchBy)
        {
            var url = 
            throw new NotImplementedException();
        }

    }
}
