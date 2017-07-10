using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace BAL
{
    public class TourBO
    {
        public static Int32 Save(TourDTO dto)
        {
            return DAL.TourDAO.Save(dto);
        }
        public static TourDTO GetTourById(Int32 tid)
        {
            return DAL.TourDAO.GetTourById(tid);
        }
        public static List<TourDTO> GetAllTours()
        {
            return DAL.TourDAO.GetAllTours();
        }
        public static Int32 DeleteTour(Int32 tid)
        {
            return DAL.TourDAO.DeleteTour(tid);
        }
    }
}
