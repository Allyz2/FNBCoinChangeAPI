using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FNBCoinChangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class svcController : ControllerBase
    {
        // GET: api/<svcController>
        [HttpGet]
        public int Get([FromQuery(Name ="Coins")]int[] coins, int amount)
        {
            Array.Sort(coins);
            // create an array to hold the x amount of coints we can have...
            //We need amount + 1 slots since this is zero based
            int[] dp = new int[amount + 1];
            // since everything is is invalid, we need to fill in the array with every thing to try 
            // sort out the subproblems
            Array.Fill(dp, amount + 1);
            // what is the minimum amount of coins we ned to make o couns
            // the answer is 0
            dp[0] = 0;

            // now to determine the fewest number of coins to make the amount....
            // we need to loop through the  amount and test each coun as we move along
            for (int i = 0; i <= amount; i++)
            {
                for (int j = 0; j < coins.Length; j++) // coints loop
                {
                    //check if the current coin is less than the current amount
                    if (coins[j] <= i)
                    {
                        // get the best minimum number of coins for the current Payout Amount
                        dp[i] = Math.Min(dp[i], 1 + dp[i - coins[j]]);
                    }
                    else
                    {
                        // no point of going through bigger and bigger coins
                        break;
                    }
                }
            }

            return dp[amount] > amount ? -1 : dp[amount];
        }




    }
}
