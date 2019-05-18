using System;
using Xunit;
using Persistence.MODEL;
using BL;
namespace BL.Test
{
    public class ItemUnitTest
    {
        private itemBl itemBl = new itemBl();
    
       
       [Fact]
       public void TestGetListItems()
       {
           Assert.NotNull(itemBl.GetListsItems());
       }
       
    }
}
