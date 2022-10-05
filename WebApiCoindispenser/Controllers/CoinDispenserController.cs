using WebApiCoindispenser.Services;
using Microsoft.AspNetCore.Mvc;
using WebApiCoindispenser.Models;

namespace WebApiCoindispenser.Controllers
{
    [ApiController]
    [Route("api/coindispenser")]
    public class CoinDispenserController : Controller
    {
        private readonly ICalculateChange _calculateChange;

        public CoinDispenserController(ICalculateChange calculateChange)
        {
            _calculateChange = calculateChange;
        }
        [HttpPost]
        public async Task<ActionResult<changesRecord>> GetChange(int changeAmount)
        {
            List<int> coins = new List<int>();
            List<int> amounts = new List<int>() { 1, 2, 5 };
            //
            // Compute change for 51 cents.
            //
            var changesRecords =_calculateChange.Change(coins, amounts, 0, 0, changeAmount);

            var mincoins = changesRecords.MinBy(X => X.totalCount);
            return Ok(mincoins);
        }
    }
}
