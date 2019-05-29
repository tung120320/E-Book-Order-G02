using System;
using Xunit;
using DAL;
using Persistence.MODEL;
namespace DAL.Test
{
    public class RatingUnitTest
    {
        private RatingDAL ratingDAL = new RatingDAL();
        // [Theory]
        // [InlineData("tung", "thanh")]
        // public void RateItemTest1(Rating rate)
        // {

        // }
        [Fact]
        public void RateItemTest()
        {
            Rating rating = new Rating(1, 1, 3, "hay", "hay qua");
            Assert.True(ratingDAL.RateItem(rating));
           
        }

        [Fact]
        public void RateItemTest1()
        {
            Rating rating = new Rating(0, 0, 3, "hay", "hay qua");
            Assert.False(ratingDAL.RateItem(rating));
        }
        // [Fact]
        // public void UpdateRateItemTest()
        // {
        //     Rating rating = new Rating(1, 1, 2, "khong hay", "sach doc chan qua");
        //     Assert.True(ratingDAL.UpdateRateItem(rating));
        // }
        [Fact]
        public void UpdateRateItemTest1()
        {
            Rating rating = new Rating(1, 0, 1, "khong hay", "sach doc chan qua");
            Assert.False(ratingDAL.UpdateRateItem(rating));
        }

    }
}
