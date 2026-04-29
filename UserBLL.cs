using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ThreeTier_Arc
{
   
    public class UserBLL // Changed from BClass
    {
        UserDAL dal = new UserDAL(); // Now matches the class name above

        public DataTable GetAllUsers(string search = "")
        {
            return dal.GetUsers(search);
        }

        public bool InsertUser(string user, string pass)
        {
            string query = "INSERT INTO UsersData1 (Username, Password) VALUES (@U, @P)";
            SqlParameter[] p = {
                new SqlParameter("@U", user),
                new SqlParameter("@P", pass)
            };
            return dal.ExecuteCommand(query, p) > 0;
        }

        public bool UpdateUser(int uid, string user, string pass)
        {
            string query = "UPDATE UsersData1 SET Username=@U, Password=@P WHERE UID=@ID";
            SqlParameter[] p = {
                new SqlParameter("@U", user),
                new SqlParameter("@P", pass),
                new SqlParameter("@ID", uid)
            };
            return dal.ExecuteCommand(query, p) > 0;
        }

        public bool DeleteUser(int uid)
        {
            string query = "DELETE FROM UsersData1 WHERE UID=@ID";
            SqlParameter[] p = { new SqlParameter("@ID", uid) };
            return dal.ExecuteCommand(query, p) > 0;
        }
    }
}