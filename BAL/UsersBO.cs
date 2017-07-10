using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace BAL
{
    public class UsersBO
    {
        public static Int32 Save(UsersDTO dto)
        {
            return DAL.UsersDAO.Save(dto);
        }
        public static UsersDTO GetUserById(Int32 rtid)
        {
            return DAL.UsersDAO.GetUserById(rtid);
        }
        public static List<UsersDTO> GetAllUsers()
        {
            return DAL.UsersDAO.GetAllUsers();
        }
        public static Int32 DeleteUser(Int32 rtid)
        {
            return DAL.UsersDAO.DeleteUser(rtid);
        }
    }
}
