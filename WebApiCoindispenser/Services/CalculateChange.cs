using WebApiCoindispenser.Models;

namespace WebApiCoindispenser.Services
{
    public class CalculateChange: ICalculateChange
    {
        List<changesRecord> changes = new List<changesRecord>();
        public async Task<changesRecord> GetListChanges(List<int> coins, List<int> amounts, int highest, int sum, int goal)
        {
            changes = new List<changesRecord>();
            changesRecord mincoins = new changesRecord();
            var listchanges = Change(coins, amounts, highest, sum, goal);
            mincoins = listchanges.MinBy(X => X.totalCount);
            if (mincoins == null) return null;
            return mincoins;
        }
        public List<changesRecord> Change(List<int> coins, List<int> amounts, int highest, int sum, int goal)
        {
           
            //
            // See if we are done.
            //
            if (sum == goal)
            {
                changesRecord changesRecord = new changesRecord();
                changesRecord = Display(coins, amounts);
                changes.Add(changesRecord);
               
                return changes;
                ;
            }
            //
            // See if we have too much.
            //
            if (sum > goal)
            {
               
                return changes;
            }
            //
            // Loop through amounts.
            //
            foreach (int value in amounts)
            {
                //
                // Only add higher or equal amounts.
                //
                if (value >= highest)
                {
                    List<int> copy = new List<int>(coins);
                    copy.Add(value);
                    Change(copy, amounts, value, sum + value, goal);
                }
            }
            return changes;

        }
        static changesRecord Display(List<int> coins, List<int> amounts)
        {
            changesRecord changesRecord = new changesRecord();
            int totalcount = 0;
            List<CoinRecord> coinRecords = new List<CoinRecord>();

            foreach (int amount in amounts)
            {
                CoinRecord CoinRecord = new CoinRecord();
                int count = coins.Count(value => value == amount);
                CoinRecord.amount = amount;
                CoinRecord.count = count;
                coinRecords.Add(CoinRecord);
                totalcount += count;
                Console.WriteLine("{0}: {1}",
                    amount,
                    count);
            }
            changesRecord.totalCount = totalcount;
            changesRecord.coins = coinRecords;
            Console.WriteLine();
            return changesRecord;
        }
    }
}
