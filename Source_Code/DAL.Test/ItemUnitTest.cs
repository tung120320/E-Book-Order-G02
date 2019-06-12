using Xunit;
using System;
using DAL;
namespace DAL.Test
{
    public class ItemUnitTest
    {

        ItemDAL itemDal = new ItemDAL();
        [Fact]
        public void GetListItemsTest()
        {
            Assert.NotNull(itemDal.GetListItems());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAnItemByIdTest(int? itemId)
        {
            Assert.NotNull(itemDal.GetAnItemById(itemId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void GetAnItemByIdTest1(int? itemId)
        {
            Assert.Null(itemDal.GetAnItemById(itemId));
        }

    }
}