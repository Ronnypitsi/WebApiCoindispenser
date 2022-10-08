using WebApiCoindispenser.Models;

namespace WebApiCoindispenser.Services
{
    public class CalculateChange: ICalculateChange
    {
        List<changesRecord> changes = new List<changesRecord>();
        /// <summary>
        /// get min record of combination coin of possible change combination, count for each coin and total coins 
        /// </summary>
        /// <param name="coins">list of coin</param>
        /// <param name="amounts">list of different denominations of coin</param>
        /// <param name="highest"></param>
        /// <param name="sum">total amount for get changes</param>
        /// <param name="goal">change amount</param>
        /// <returns></returns>
        public async Task<changesRecord> GetListChanges(List<int> coins, List<int> amounts, int highest, int sum, int goal)
        {
            changes = new List<changesRecord>();
            changesRecord mincoins = new changesRecord();
            var listchanges = Change(coins, amounts, highest, sum, goal);
            //get minimum combination of coins required to make change
            mincoins = listchanges.MinBy(X => X.totalCount);
            if (mincoins == null) return null;
            return mincoins;
        }
        /// <summary>
        /// get list of possible change combination, count for each coin and total coins 
        /// </summary>
        /// <param name="coins">list of coin</param>
        /// <param name="amounts">list of different denominations of coin</param>
        /// <param name="highest"></param>
        /// <param name="sum">total amount for get changes</param>
        /// <param name="goal">change amount</param>
        /// <returns></returns>
        public List<changesRecord> Change(List<int> coins, List<int> amounts, int highest, int sum, int goal)
        {
           
            //
            // See if we are done.
            //
            if (sum == goal)
            {
                changesRecord changesRecord = new changesRecord();
                changesRecord = AddChangesRecord(coins, amounts);
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
        /// <summary>
        /// Add possible combination of coin and total count to the list
        /// </summary>
        /// <param name="coins">list of coins</param>
        /// <param name="amounts">list of amount</param>
        /// <returns></returns>
        static changesRecord AddChangesRecord(List<int> coins, List<int> amounts)
        {
            changesRecord changesRecord = new changesRecord();
            int totalcount = 0;
            List<CoinRecord> coinRecords = new List<CoinRecord>();

            foreach (int amount in amounts)
            {
                CoinRecord CoinRecord = new CoinRecord();
                //get count of each coin
                int count = coins.Count(value => value == amount);
                CoinRecord.amount = amount;
                CoinRecord.count = count;
                coinRecords.Add(CoinRecord);
                totalcount += count;
                //Console.WriteLine("{0}: {1}",
                //    amount,
                //    count);
            }
            changesRecord.totalCount = totalcount;
            changesRecord.coins = coinRecords;
            //Console.WriteLine();
            return changesRecord;
        }
    }
}
