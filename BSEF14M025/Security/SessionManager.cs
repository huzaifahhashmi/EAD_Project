using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace BSEF14M025.Security
{
    public class SessionManager
    {
        public static UsersDTO User
        {
            get
            {
                UsersDTO dto = null;
                if (HttpContext.Current.Session["User"] != null)
                {
                    dto = HttpContext.Current.Session["User"] as UsersDTO;
                }
                return dto;
            }
            set
            {
                HttpContext.Current.Session["User"] = value;
            }
        }

        public static Boolean IsValidUser
        {
            get
            {
                if (User != null)
                    return true;
                else
                    return false;
            }
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }
    }
}