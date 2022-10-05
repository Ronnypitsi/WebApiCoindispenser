using WebApiCoindispenser.Models;

namespace WebApiCoindispenser.Services
{
    public interface ICalculateChange
    {
        List<changesRecord> Change(List<int> coins, List<int> amounts, int highest, int sum, int goal);
    }
}
