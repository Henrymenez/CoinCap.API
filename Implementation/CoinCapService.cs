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
            try
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
                        HttpStatusCode = 200,
                        IsSuccess = true,
                        Message = "Successful"
                    };
                }

                return new Result<ClientResponse>
                {
                    IsSuccess = false,
                    HttpStatusCode = 500,
                    Message = "Requst could not be completed at this time"
                };
            }
            catch (Exception ex)
            {
                return new Result<ClientResponse>
                {
                    IsSuccess = false,
                    HttpStatusCode = 500,
                    Message = ex.Message
                };
            }
        }




    }
}
