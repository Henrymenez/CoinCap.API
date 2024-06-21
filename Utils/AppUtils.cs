using CoinCap.API.Dtos.Response;

namespace CoinCap.API.Utils
{
    public static class AppUtils
    {
        public static async Task<HttpResponseMessage> clientCall(string url)
        {
            var client = new HttpClient();
            var getCryptoInfo = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(getCryptoInfo);
            return response;
        }

        public static CoinCapBaseResponse SearchCryptoByName(ClientResponse cryptoData, string name)
        {
            return cryptoData.Data.FirstOrDefault(crypto => crypto.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!;
        }
    }
}
