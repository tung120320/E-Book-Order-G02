using System;
using Persistence.MODEL;
using MySql.Data.MySqlClient;
namespace DAL
{

    public class UsersDAL
    {
        public Users GetUserInfo()
        {
            string query = $@"select * FROM users";
            DbHelper.OpenConnection();
            MySqlDataReader reader = DbHelper.ExecuteQuery(query);
            Users u = null;
            if (reader.Read())
            {
                u = GetUsersInfo(reader);
            }
            DbHelper.CloseConnection();
            return u;
        }

        private static Users GetUsersInfo(MySqlDataReader reader){
            Users user = new Users();
            user.userId = reader.GetInt32("userId");
            user.userName = reader.GetString("userName");
            user.userPassword = reader.GetString("userPassword");
            user.nameUser = reader.GetString("nameUser");
            user.userEmail = reader.GetString("userEmail");
            user.userIdCardNo = reader.GetInt32("userIdCardNo");
            user.userBalance = reader.GetFloat("userBalance");
            
            return user;
        }
    }

}
