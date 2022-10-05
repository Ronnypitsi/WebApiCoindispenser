using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebApiCoindispenser.Controllers;
using WebApiCoindispenser.Models;
using WebApiCoindispenser.Services;

namespace TestProject2
{
    public class coinsrequiredchangetTest
    {
        [Fact]
        public async void coins_required_to_make_change()
        {
            // Arrange
            int changeAmount = 10;
            var ical = A.Fake<ICalculateChange>();
            var controller = new CoinDispenserController(ical);

            // Act
            var actionResult = await controller.GetChange(changeAmount);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var changeResult = result.Value as changesRecord;
            Assert.Equal(2, changeResult.totalCount);


        }
    }
}