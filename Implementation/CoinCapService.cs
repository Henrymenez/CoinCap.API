using CoinCap.API.Dtos.Response;
using CoinCap.API.Interface;
using Newtonsoft.Json;

namespace CoinCap.API.Implementation
{
    public class CoinCapService : ICoinCapService
    {
        public CoinCapService()
        {

        }

        public async Task<Result<ClientResponse>> GetAllCryptoAvailable(string? searchBy,int pageSize, int pageNumber)
        {

            var infoMessage = await clientCall();
            if (infoMessage.IsSuccessStatusCode)
            {
                var stringContent = await infoMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ClientResponse>(stringContent);
                // Pagination settings
                /*result.PageNumber = 2; // Get the second page
                result.PageSize = 2; // Items per page*/
                if (!string.IsNullOrEmpty(searchBy))
                 {
                    var searchresult = SearchCryptoByName(result, searchBy);

                    if (searchresult != null)
                    {
                        // Replace cryptoData.Data with the single search result encapsulated in an IEnumerable
                        result.ReplaceDataWithSingleResult(searchresult);
                    }
                }

                result.Data = result.GetPaginatedData(pageSize,pageNumber);



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

        private async Task<HttpResponseMessage> clientCall()
        {
            var url = "https://api.coincap.io/v2/assets";
            var client = new HttpClient();
            var getCryptoInfo = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(getCryptoInfo);
            return response;
        }
        private static CoinCapBaseResponse? SearchCryptoByName(ClientResponse cryptoData, string name)
        {
            return cryptoData.Data.FirstOrDefault(crypto => crypto.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
