﻿using System.Net;

namespace CoinCap.API.Dtos.Response
{
    public record Result<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public int HttpStatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
