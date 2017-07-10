using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities;
namespace DAL
{
    public static class AdminDAO
    {
        public static Int32 Save(AdminDTO dto)
        {
            String sqlQuery = "";
            using (DBHelper helper = new DBHelper())
            {
                if (dto.AdminID > 0)
                {
                    sqlQuery = String.Format("Update dbo.Admin Set UserName='{0}', Password='{1}', Name='{2}' Where AdminID='{3}'",
                        dto.UserName, dto.Password, dto.Name, dto.AdminID);
                    helper.ExecuteQuery(sqlQuery);
                    return dto.AdminID;
                }
                else
                {
                    sqlQuery = String.Format("INSERT INTO dbo.Admin(UserName, Password, Name) VALUES('{0}', '{1}', '{2}'); SELECT @@IDENTITY",
                        dto.UserName, dto.Password, dto.Name);
                    var obj = helper.ExecuteScalar(sqlQuery);
                    return Convert.ToInt32(obj);
                }


            }
        }
        public static AdminDTO GetAdminById(Int32 aid)
        {
            var query = String.Format("Select * from dbo.Admin Where AdminID='{0}'", aid);
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                AdminDTO dto = null;
                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }
                return dto;
            }
        }
        public static List<AdminDTO> GetAllAdmins()
        {
            var query = "Select * from dbo.Admin;";
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                List<AdminDTO> list = new List<AdminDTO>();
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
        public static Int32 DeleteAdmin(Int32 aid)
        {
            String sqlQuery = String.Format("DELETE FROM dbo.Admin WHERE AdminID='{0}'", aid);
            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }
        private static AdminDTO FillDTO(SqlDataReader reader)
        {
            var dto = new AdminDTO();
            dto.AdminID = reader.GetInt32(0);
            dto.UserName = reader.GetString(1);
            dto.Password = reader.GetString(2);
            dto.Name = reader.GetString(3);
            return dto;
        }
    }
}
