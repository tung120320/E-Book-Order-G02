using System;
using Xunit;
using DAL;
namespace DAL.Test{
    public class DBHelperUnitTest{
        [Fact]
        public void OpenConnectionTest()
        {
            Assert.NotNull(DbHelper.OpenConnection());
        }
    }
}
