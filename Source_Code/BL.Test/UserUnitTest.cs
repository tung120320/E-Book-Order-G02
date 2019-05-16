using System;
using Xunit;
using Persistence.MODEL;
using BL;
namespace BL.Test
{
    public class UnitTest1
    {
        private UserBL userBL = new UserBL();
    
       
        [Theory]
        [InlineData("tung", "thanh")]
        public void LoginTest1(string username, string password)
        {
            User user = userBL.Login(username, password);
            Assert.NotNull(user);
            Assert.Equal(username, user.UserAccount);
            Assert.Equal(password, user.UserPassword);
            
        }
        // [Theory]
        // [InlineData("'?/:%'", "'.:=='")]
        // [InlineData("tung1203", "1324567898456")]
        // [InlineData(null, "'.:=='")]
        // [InlineData("'.:=='", null)]
        // public void LoginTest2(string username, string password)
        // {
        //     Assert.Null(userBL.Login(username, password));
        // }

    }
}
