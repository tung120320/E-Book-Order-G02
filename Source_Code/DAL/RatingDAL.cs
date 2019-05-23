using System;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
using System.Collections.Generic;
namespace DAL
{
    public class RatingDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;
        public RatingDAL()
        {
            connection = DbHelper.OpenConnection();
        }

        public bool Rating(Rating rate)
        {

            if (rate == null)
            {
                return false;
            }
            if (connection == null)
            {
                connection = DbHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection = DbHelper.OpenConnection();
            }
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $@"insert into Ratings values(@itemId,@userId,@ratingStart,@ratingTitle,@ratingContent,@ratingDate)";
            command.Parameters.AddWithValue("@itemId", rate.ItemId);
            command.Parameters.AddWithValue("@userId", rate.UserId);
            command.Parameters.AddWithValue("@ratingStart", rate.RatingStart);
            command.Parameters.AddWithValue("@ratingTitle", rate.RatingTitle);
            command.Parameters.AddWithValue("@ratingContent", rate.RatingContent);
            command.Parameters.AddWithValue("@ratingDate", rate.RatingDate);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
        public List<Rating> GetAllRating(int? itemId)
        {
            if (itemId == null)
            {
                return null;
            }
            if (connection == null)
            {
                connection = DbHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection = DbHelper.OpenConnection();
            }
            query = $@"select * from ratings where itemId = {itemId}";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<Rating> listRatings = null;
            using (reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listRatings.Add(GetRating());
                }
            }
            connection.Close();
            return listRatings;
        }
        private Rating GetRating()
        {
            Rating rating = new Rating();
            rating.ItemId = reader.GetInt32("itemId");
            rating.UserId = reader.GetInt32("userId");
            rating.RatingStart = reader.GetInt32("ratingStart");
            rating.RatingTitle = reader.GetString("ratingTitle");
            rating.RatingContent = reader.GetString("ratingContent");
            rating.RatingDate = reader.GetDateTime("ratingDate");
            return rating;
        }

    }
}