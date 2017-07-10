using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities;
namespace DAL
{
    public static class RegisteredTravellerDAO
    {
        public static Int32 Save(RegisteredTravellerDTO dto)
        {
            String sqlQuery = "";
            using (DBHelper helper = new DBHelper())
            {
                if (dto.RTID > 0)
                {
                    sqlQuery = String.Format("Update dbo.RegisteredTraveller Set TourID='{0}', UserID='{1}' Where RTID='{3}'",
                        dto.TourID, dto.UserID, dto.RTID);
                    helper.ExecuteQuery(sqlQuery);
                    return dto.RTID;
                }
                else
                {
                    sqlQuery = String.Format("INSERT INTO dbo.RegisteredTraveller(TourID, UserID) VALUES('{0}', '{1}'); SELECT @@IDENTITY",
                        dto.TourID, dto.UserID);
                    var obj = helper.ExecuteScalar(sqlQuery);
                    return Convert.ToInt32(obj);
                }


            }
        }
        public static RegisteredTravellerDTO GetRegisteredTravellerById(Int32 rtid)
        {
            var query = String.Format("Select * from dbo.RegisteredTraveller Where RTID='{0}'", rtid);
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                RegisteredTravellerDTO dto = null;
                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }
                return dto;
            }
        }
        public static List<RegisteredTravellerDTO> GetAllRegisteredTravellers()
        {
            var query = "Select * from dbo.RegisteredTraveller;";
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                List<RegisteredTravellerDTO> list = new List<RegisteredTravellerDTO>();
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
        public static Int32 DeleteRegisteredTraveller(Int32 rtid)
        {
            String sqlQuery = String.Format("DELETE FROM dbo.RegisteredTraveller WHERE RTID='{0}'", rtid);
            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }
        private static RegisteredTravellerDTO FillDTO(SqlDataReader reader)
        {
            var dto = new RegisteredTravellerDTO();
            dto.RTID = reader.GetInt32(0);
            dto.TourID = reader.GetInt32(1);
            dto.UserID = reader.GetInt32(2);
            return dto;
        }
    }
}
