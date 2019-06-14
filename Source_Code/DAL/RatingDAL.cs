using System;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
using System.Collections.Generic;
namespace DAL
{
    public class RatingDAL
    {
        private MySqlDataReader reader;
        private string query;

        public RatingDAL() { }

        public bool RateItem(Rating rating)
        {
            if (rating == null)
            {
                return false;
            }

            query = $@"insert into Ratings values
            ({rating.ItemId},{rating.UserId},'{rating.RatingStars}','{rating.RatingTitle}','{rating.RatingContent}',NOW());";
            try
            {
                DbHelper.ExecNonQuery(query, DbHelper.OpenConnection());
            }
            catch (System.Exception)
            {

                return false;
            }
            finally
            {
                DbHelper.CloseConnection();

            }

            return true;
        }
        public bool UpdateRateItem(Rating rating)
        {
            if (rating == null)
            {
                return false;
            }

            query = $@"UPDATE Ratings 
            SET ratingStars = {rating.RatingStars}, ratingTitle = '{rating.RatingTitle}', ratingContent = '{rating.RatingContent}', ratingDate = NOW()
            WHERE itemID = {rating.ItemId} and userID = {rating.UserId};";

            try
            {
                int numberEffect = DbHelper.ExecNonQuery(query, DbHelper.OpenConnection());
                if (numberEffect == 0)
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                DbHelper.CloseConnection();
            }

            return true;
        }
        public List<Rating> GetAllRating(int? itemId)
        {
            if (itemId == null)
            {
                return null;
            }

            query = $@"select * from ratings where itemId = {itemId}";
            reader = DbHelper.ExecQuery(query, DbHelper.OpenConnection());
            List<Rating> listRatings = new List<Rating>();
            while (reader.Read())
            {
                listRatings.Add(GetRating(reader));
            }

            DbHelper.CloseConnection();
            return listRatings;
        }
        public Rating CheckItemRatedByUserId(int? userId, int? itemId)
        {
            if (userId == null)
            {
                return null;
            }

            query = $@"SELECT * FROM ratings where userId ={userId} and itemId = {itemId};";

            try
            {
                reader = DbHelper.ExecQuery(query, DbHelper.OpenConnection());
            }
            catch (System.Exception)
            {
                Console.WriteLine("Không thể kết nối tới cơ sở dữ liệu");
                return null;
            }
            Rating rating = null;
            if (reader.Read())
            {
                rating = GetRating(reader);
            }
            reader.Close();
            DbHelper.CloseConnection();

            return rating;
        }
        private Rating GetRating(MySqlDataReader reader)
        {
            Rating rating = new Rating();
            rating.ItemId = reader.GetInt32("itemId");
            rating.UserId = reader.GetInt32("userId");
            rating.RatingStars = reader.GetInt32("ratingStars");
            rating.RatingTitle = reader.GetString("ratingTitle");
            rating.RatingContent = reader.GetString("ratingContent");
            rating.RatingDate = reader.GetDateTime("ratingDate");
            return rating;
        }

    }
}