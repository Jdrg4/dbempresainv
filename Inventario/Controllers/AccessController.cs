using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                using (dbempresainvEntities db = new dbempresainvEntities())
                {
                    var lst = from d in db.user
                              where d.correo == user.Trim() && d.password == password.Trim() && d.idState == 1
                              select d;
                    if (lst.Count() > 0)
                    {
                        user oUser = lst.FirstOrDefault();
                        Session["User"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario no válido :(");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content("Ocurrió un error :(" + ex.Message);
            }
        }
    }    
}