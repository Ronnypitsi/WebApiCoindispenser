using WebApiCoindispenser.Models;

namespace WebApiCoindispenser.Services
{
    public interface ICalculateChange
    {
        Task<changesRecord> GetListChanges(List<int> coins, List<int> amounts, int highest, int sum, int goal);
        
    }
}
