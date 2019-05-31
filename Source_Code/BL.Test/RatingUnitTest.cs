using System;
using Xunit;
using BL;
using Persistence.MODEL;
using MySql.Data.MySqlClient;
namespace DAL.Test
{
    public class RatingUnitTest
    {   
       
        private RatingBL ratingBL = new RatingBL();
     
        [Fact]
        public void RateItemTest()
        {
            Rating rating = new Rating(1, 2, 3, "hay", "hay qua");
            Assert.True(ratingBL.RateItem(rating));  
            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"DELETE FROM ratings where itemId = {1} and userId = {2}";
            command.ExecuteNonQuery();         
        }

        [Fact]
        public void RateItemTest1()
        {
            Rating rating = new Rating(0, 0, 3, "hay", "hay qua");
            Assert.False(ratingBL.RateItem(rating));
        }
        [Fact]
        public void UpdateRateItemTest()
        {
            Rating rating1 = new Rating(1, 1, 3, "hay", "hay qua");
            ratingBL.RateItem(rating1);
            Rating rating = new Rating(1, 1, 2, "khong hay", "sach doc chan qua");
            Assert.True(ratingBL.UpdateRateItem(rating));
            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"DELETE FROM ratings where itemId = {1} and userId = {1}";
            command.ExecuteNonQuery();
        }
        [Fact]
        public void UpdateRateItemTest1()
        {
            Rating rating = new Rating(0, 0, 1, "khong hay", "sach doc chan qua");
            Assert.False(ratingBL.UpdateRateItem(rating));
        }
        [Fact]
        public void GetAllRatingTest()
        {
           Assert.NotNull(ratingBL.GetAllRating(1));
        }

    }
}
