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
       public void TestGetListItems()
       {
           Assert.NotNull(itemBl.GetListsItems());
       }
       
    }
}
