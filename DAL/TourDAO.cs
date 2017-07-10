using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entities;
namespace DAL
{
    public static class TourDAO
    {
        public static Int32 Save(TourDTO dto)
        {
            String sqlQuery = "";
            using (DBHelper helper = new DBHelper())
            {
                if (dto.TourID > 0)
                {
                    sqlQuery = String.Format("Update dbo.Tour Set FromCity='{0}', ToCity='{1}', SubDeadLine='{2}', Departure='{3}', ReturnDate='{4}', Dues='{5}' Where TourID='{6}'",
                        dto.FromCity, dto.ToCity, dto.SubDeadLine, dto.Departure, dto.ReturnDate, dto.Dues, dto.TourID);
                    helper.ExecuteQuery(sqlQuery);
                    return dto.TourID;
                }
                else
                {
                    sqlQuery = String.Format("INSERT INTO dbo.Tour (FromCity, ToCity, SubDeadLine, Departure, ReturnDate, Dues) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');",
                        dto.FromCity, dto.ToCity, dto.SubDeadLine, dto.Departure, dto.ReturnDate, dto.Dues);
                    var obj = helper.ExecuteScalar(sqlQuery);
                    return Convert.ToInt32(obj);
                }


            }
        }
        public static TourDTO GetTourById(Int32 tid)
        {
            var query = String.Format("Select * from dbo.Tour Where TourID='{0}'", tid);
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                TourDTO dto = null;
                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }
                return dto;
            }
        }
        public static List<TourDTO> GetAllTours()
        {
            var query = "Select * from dbo.Tour;";
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);
                List<TourDTO> list = new List<TourDTO>();
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
        public static Int32 DeleteTour(Int32 tid)
        {
            String sqlQuery = String.Format("DELETE FROM dbo.Tour WHERE TourID='{0}'", tid);
            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }
        private static TourDTO FillDTO(SqlDataReader reader)
        {
            var dto = new TourDTO();
            dto.TourID = reader.GetInt32(0);
            dto.FromCity = reader.GetString(1);
            dto.ToCity = reader.GetString(2);
            dto.SubDeadLine = reader.GetDateTime(3);
            dto.Departure = reader.GetDateTime(4);
            dto.ReturnDate = reader.GetDateTime(5);
            dto.Dues = reader.GetInt32(6);
            return dto;
        }
    }
}
