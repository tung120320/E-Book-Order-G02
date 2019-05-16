using System;
using Xunit;
using DAL;
using Persistence.MODEL;
namespace DAL.Test
{
    public class UserUnitTest
    {
        private UserDAL userDAL = new UserDAL();
        [Theory]
        [InlineData("tung", "thanh")]
        public void LoginTest1(string username, string password)
        {
            User user = userDAL.Login(username, password);
            Assert.NotNull(user);
            Assert.Equal(username, user.UserAccount);
            Assert.Equal(password, user.UserPassword);
        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("tung1203", "1324567898456")]
        [InlineData(null, "'.:=='")]
        [InlineData("'.:=='", null)]
        public void LoginTest2(string username, string password)
        {
            Assert.Null(userDAL.Login(username, password));
        }
    }
}
