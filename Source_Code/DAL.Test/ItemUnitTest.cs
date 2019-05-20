using Xunit;
using System;
using DAL;
namespace DAL.Test
{
    public class ItemUnitTest
    {
        ItemDAL itemDal = new ItemDAL();
        [Fact]
        public void GetItems()
        {
            Assert.NotNull(itemDal.GetListsItems());
        }
        [Fact]
        public void GetAnItem()
        {
            Assert.NotNull(itemDal.GetAnItem(1));
        }  
        [Fact]
        public void GetAnItem1()
        {
            Assert.Null(itemDal.GetAnItem(0));
        }
    }
}