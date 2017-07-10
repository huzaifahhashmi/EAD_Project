using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities;
namespace DAL
{
    public static class UsersDAO
    {
        public static Int32 Save(UsersDTO dto)
        {
            String sqlQuery = "";
            using (DBHelper helper = new DBHelper())
            {
                if (dto.UserID > 0)
                {
                    sqlQuery = String.Format("Update dbo.Users Set UserName='{0}', Password='{1}', Email='{2}', PhoneNumber='{3}' Where UserID='{4}'",
                        dto.UserName, dto.Password, dto.Email, dto.PhoneNumber, dto.UserID);
                    helper.ExecuteQuery(sqlQuery);
                    return dto.UserID;
                }
                else
                {
                    sqlQuery = String.Format("INSERT INTO dbo.Users(UserName, Password, Email, PhoneNumber) VALUES('{0}', '{1}', '{2}', '{3}'); SELECT @@IDENTITY",
                        dto.UserName, dto.Password, dto.Email, dto.PhoneNumber);
                    var obj = helper.ExecuteScalar(sqlQuery);
                    return Convert.ToInt32(obj);
                }


            }
        }
        public static UsersDTO GetUserById(Int32 uid)
        {
            var query = String.Format("Select * from dbo.Users Where UserID='{0}'", uid);
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                UsersDTO dto = null;
                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }
                return dto;
            }
        }
        public static List<UsersDTO> GetAllUsers()
        {
            var query = "Select * from dbo.Users;";
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                List<UsersDTO> list = new List<UsersDTO>();
                while (reader.Read())
                {
                    var dto = FillDTO(reader);
                    if (dto != null)
                    {
                        list.Add(dto);
                    }
                }
                return list;
            }
        }
        public static Int32 DeleteUser(Int32 uid)
        {
            String sqlQuery = String.Format("DELETE FROM dbo.Users WHERE UserID='{0}'", uid);
            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }
        private static UsersDTO FillDTO(SqlDataReader reader)
        {
            var dto = new UsersDTO();
            dto.UserID = reader.GetInt32(0);
            dto.UserName = reader.GetString(1);
            dto.Password = reader.GetString(2);
            dto.Email = reader.GetString(3);
            dto.PhoneNumber = reader.GetString(4);
            return dto;
        }
    }
}
