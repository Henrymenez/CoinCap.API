using Newtonsoft.Json;

namespace CoinCap.API.Dtos.Response
{
    public record ClientResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        [JsonProperty("data")]
        public IEnumerable<CoinCapBaseResponse> Data { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        public void ReplaceDataWithSingleResult(CoinCapBaseResponse result)
        {
            this.Data = new List<CoinCapBaseResponse> { result };
        }
        

        public IEnumerable<CoinCapBaseResponse> GetPaginatedData(int pageSize,int pageNumber)
        {
            int skipCount = (pageNumber - 1) * pageSize;
            PageSize = pageSize;
            PageNumber = pageNumber;
            return Data.Skip(skipCount).Take(pageSize);
        }
    }
}
