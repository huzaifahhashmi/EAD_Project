using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace BAL
{
    public class AdminBO
    {
        public static Int32 Save(AdminDTO dto)
        {
            return DAL.AdminDAO.Save(dto);
        }
        public static AdminDTO GetAdminById(Int32 aid)
        {
            return DAL.AdminDAO.GetAdminById(aid);
        }
        public static List<AdminDTO> GetAllAdmins()
        {
            return DAL.AdminDAO.GetAllAdmins();
        }
        public static Int32 DeleteAdmin(Int32 aid)
        {
            return DAL.AdminDAO.DeleteAdmin(aid);
        }
    }
}
