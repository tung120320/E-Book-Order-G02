using System;
using Xunit;
using DAL;
using Persistence.MODEL;
using MySql.Data.MySqlClient;
namespace DAL.Test
{
    public class RatingUnitTest
    {   
       
        private RatingDAL ratingDAL = new RatingDAL();
     
        [Fact]
        public void RateItemTest()
        {
            Rating rating = new Rating(9, 1, 3, "hay", "hay qua");
            Assert.True(ratingDAL.RateItem(rating));  
            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"DELETE FROM ratings where itemId = {9} and userId = {2}";
            command.ExecuteNonQuery();         
        }

        [Fact]
        public void RateItemTest1()
        {
            Rating rating = new Rating(0, 0, 3, "hay", "hay qua");
            Assert.False(ratingDAL.RateItem(rating));
        }
        [Fact]
        public void UpdateRateItemTest()
        {
            Rating rating1 = new Rating(9, 1, 3, "hay", "hay qua");
            ratingDAL.RateItem(rating1);
            Rating rating = new Rating(9, 1, 2, "khong hay", "sach doc chan qua");
            Assert.True(ratingDAL.UpdateRateItem(rating));
            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"DELETE FROM ratings where itemId = {9} and userId = {1}";
            command.ExecuteNonQuery();
        }
        [Fact]
        public void UpdateRateItemTest1()
        {
            Rating rating = new Rating(0, 0, 1, "khong hay", "sach doc chan qua");
            Assert.False(ratingDAL.UpdateRateItem(rating));
        }
        [Fact]
        public void GetAllRatingTest()
        {
           Assert.NotNull(ratingDAL.GetAllRating(1));
        }

    }
}
