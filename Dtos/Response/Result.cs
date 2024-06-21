namespace CoinCap.API.Dtos.Response
{
    public record Result<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
