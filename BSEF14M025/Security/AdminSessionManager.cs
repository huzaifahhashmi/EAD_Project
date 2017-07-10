using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace BSEF14M025.Security
{
    public class AdminSessionManager
    {
        public static AdminDTO Admin
        {
            get
            {
                AdminDTO dto = null;
                if (HttpContext.Current.Session["Admin"] != null)
                {
                    dto = HttpContext.Current.Session["Admin"] as AdminDTO;
                }
                return dto;
            }
            set
            {
                HttpContext.Current.Session["Admin"] = value;
            }
        }

        public static Boolean IsValidAdmin
        {
            get
            {
                if (Admin != null)
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