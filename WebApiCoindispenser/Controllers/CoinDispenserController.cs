using WebApiCoindispenser.Services;
using Microsoft.AspNetCore.Mvc;
using WebApiCoindispenser.Models;
using Microsoft.AspNetCore.Cors;

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
        [HttpGet]
        [EnableCors("corsapp")]
        [Route("Get-Coins-Required-For-Change/{changeAmount}")]
        //[ProducesResponseType(200, Type= typeof(changesRecord))]
        public  ActionResult<changesRecord> GetCoinsRequiredForChange(int changeAmount)
        {
            List<int> coins = new List<int>();
            List<int> amounts = new List<int>() { 1, 2, 5 };
            //
            // Compute change for 51 cents.
            //
            var mincoins = _calculateChange.GetListChanges(coins, amounts, 0, 0, changeAmount);
           if(mincoins==null)
                return BadRequest();
            return Ok(mincoins);
        }
    }
}

