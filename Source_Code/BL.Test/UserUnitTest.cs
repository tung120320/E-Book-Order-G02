using System;
using Xunit;
using Persistence.MODEL;
using BL;
namespace BL.Test
{
    public class UserUnitTest
    {
        private UserBL userBL = new UserBL();


        [Theory]
        [InlineData("tung", "thanh")]
        public void GetUserByUserNameAndPassWordTest(string username, string password)
        {
            User user = userBL.GetUserByUserNameAndPassWord(username, password);
            Assert.NotNull(user);
            Assert.Equal(username, user.UserAccount);
            Assert.Equal(password, user.UserPassword);
        }
        [Theory]
        [InlineData("'?/:%'", "'.:=='")]
        [InlineData("tung1203", "1324567898456")]
        [InlineData("null", "'.:=='")]
        [InlineData("'.:=='", "null")]
        public void GetUserByUserNameAndPassWordTest1(string username, string password)
        {
            Assert.Null(userBL.GetUserByUserNameAndPassWord(username, password));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUserByIdTest(int? userId)
        {
            Assert.NotNull(userBL.GetUserById(userId));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]

        public void GetUserByIdTest1(int? userId)
        {
            Assert.Null(userBL.GetUserById(userId));
        }

    }
}
