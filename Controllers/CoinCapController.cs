using CoinCap.API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoinCap.API.Controllers
{
    [ApiController]
    
    public class CoinCapController : ControllerBase
    {
        private readonly ICoinCapService _coinCapService;

        public CoinCapController(ICoinCapService coinCapService)
        {
            _coinCapService = coinCapService;
        }

        [HttpGet]
        [Route("coins")]
        public async Task<IActionResult> GetCoins(string? searchBy, int? pageSize = 10, int? pageNumber = 1)
        {
            var result = await _coinCapService.GetAllCryptoAvailable(searchBy,pageSize.Value,pageNumber.Value);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
           return StatusCode(result.HttpStatusCode,result);
        }
    }
}
