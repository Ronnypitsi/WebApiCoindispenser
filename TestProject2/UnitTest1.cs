using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApiCoindispenser.Controllers;
using WebApiCoindispenser.Models;
using WebApiCoindispenser.Services;

namespace TestProject2
{
    public class coinsrequiredchangetTest
    {


        private readonly ICalculateChange _calculateChange;
        
        public coinsrequiredchangetTest()
        {
            _calculateChange = A.Fake<ICalculateChange>();
        }
        
        [Fact]
       
        public async void coins_required_to_make_change()
        {
            // Arrange
            var isCreatedInvokedInRepository = false;
            int changeAmount = 10;
            var changesRecord = A.Fake<changesRecord>();
            var amounts = A.Fake<List<int>>();
            var coins = A.Fake<List<int>>();
            A.CallTo(() => _calculateChange.GetListChanges(coins, amounts, 0, 0, changeAmount)).Returns(changesRecord);
            var controller = new CoinDispenserController(_calculateChange);
            // Act

            var result = controller.GetCoinsRequiredForChange(changeAmount);

            // Assert

           var  resultsV = result.Result as  OkObjectResult;
            Assert.NotNull(resultsV);


        }
    }
}