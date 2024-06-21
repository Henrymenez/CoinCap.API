using System.Net;

namespace CoinCap.API.Dtos.Response
{
    public record ErrorResult
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode {get;set;}
        public string Message { get; set; } = string.Empty;
    }
}
