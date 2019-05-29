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
        public void GetUserByUserNameAndPassWordTest(string username, string password)
        {
            User user = userDAL.GetUserByUserNameAndPassWord(username, password);
            Assert.NotNull(user);
            Assert.Equal(username, user.UserAccount);
            Assert.Equal(password, user.UserPassword);
        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("tung1203", "1324567898456")]
        [InlineData("saadasd", "saadasd")]
        [InlineData("adassad", "dasq")]
        // [InlineData("null", "'.:=='")]
        // [InlineData("'.:=='", "null")]
        public void GetUserByUserNameAndPassWordTest1(string username, string password)
        {
            Assert.Null(userDAL.GetUserByUserNameAndPassWord(username, password));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUserByIdTest(int? userId)
        {
            Assert.NotNull(userDAL.GetUserById(userId));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        
        public void GetUserByIdTest1(int? userId)
        {
            Assert.Null(userDAL.GetUserById(userId));
        }
    }
}
