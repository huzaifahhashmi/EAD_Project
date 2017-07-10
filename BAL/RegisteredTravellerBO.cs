using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace BAL
{
    public class RegisteredTravellerBO
    {
        public static Int32 Save(RegisteredTravellerDTO dto)
        {
            return DAL.RegisteredTravellerDAO.Save(dto);
        }
        public static RegisteredTravellerDTO GetRegisteredTravellerById(Int32 rtid)
        {
            return DAL.RegisteredTravellerDAO.GetRegisteredTravellerById(rtid);
        }
        public static List<RegisteredTravellerDTO> GetAllRegisteredTravellers()
        {
            return DAL.RegisteredTravellerDAO.GetAllRegisteredTravellers();
        }
        public static Int32 DeleteRegisteredTraveller(Int32 rtid)
        {
            return DAL.RegisteredTravellerDAO.DeleteRegisteredTraveller(rtid);
        }
    }
}
