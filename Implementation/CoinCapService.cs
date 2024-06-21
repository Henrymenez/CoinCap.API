using CoinCap.API.Dtos.Response;
using CoinCap.API.Infrastructure;
using CoinCap.API.Interface;
using CoinCap.API.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoinCap.API.Implementation
{
    public class CoinCapService : ICoinCapService
    {
        private readonly CoinCapConfig _Config;

        public CoinCapService(IOptions<CoinCapConfig> Config)
        {
            _Config = Config.Value;
        }

        public async Task<Result<ClientResponse>> GetAllCryptoAvailable(string? searchBy, int pageSize, int pageNumber)
        {
            string url = _Config.CoinCapUrl;
            var infoMessage = await AppUtils.clientCall(url);
            if (infoMessage.IsSuccessStatusCode)
            {
                var stringContent = await infoMessage.Content.ReadAsStringAsync();
                ClientResponse result = JsonConvert.DeserializeObject<ClientResponse>(stringContent)!;

                if (!string.IsNullOrEmpty(searchBy))
                {
                    var searchresult = AppUtils.SearchCryptoByName(result, searchBy);

                    if (searchresult != null)
                    {
                        result.ReplaceDataWithSingleResult(searchresult);
                    }
                }

                result.Data = result.GetPaginatedData(pageSize, pageNumber);

                return new Result<ClientResponse>
                {
                    Data = result,
                    IsSuccess = true,
                    Message = "Successful"
                };
            }

            return new Result<ClientResponse>
            {
                IsSuccess = false,
                Message = "Failed to complete requst"
            };
        }


       

    }
}
