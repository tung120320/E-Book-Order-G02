using System;
using Xunit;
using Persistence.MODEL;
using BL;
namespace BL.Test
{
    public class ItemUnitTest
    {
        private ItemBl itemBl = new ItemBl();
    
       
      [Fact]
        public void GetListItemsTest()
        {
            Assert.NotNull(itemBl.GetListItems());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAnItemByIdTest(int? itemId)
        {
            Assert.NotNull(itemBl.GetAnItemById(itemId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void GetAnItemByIdTest1(int? itemId)
        {
            Assert.Null(itemBl.GetAnItemById(itemId));
        }
       
    }
}
