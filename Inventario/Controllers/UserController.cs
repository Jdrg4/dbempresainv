using Inventario.Models.TableViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventario.Models;
using Inventario.Models.ViewModels;
using Inventario.Filters;

namespace Inventario.Controllers
{
    public class UserController : Controller
    {
        #region Index
        // GET: User
        [AuthorizeUser(idOperacion:4)]
        public ActionResult Index()
        {
            List<UserTableViewModel> lst = null;

            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                lst = (from d in db.user
                       where d.idState == 1
                       orderby d.id
                       select new UserTableViewModel
                       {
                           Id = d.id,
                           Documento = d.documento,
                           Nombre = d.nombre,
                           Apellido = d.apellido,
                           Correo = d.correo
                       }).ToList();
            }
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View(lst);
        }
        #endregion

        #region Add
        [AuthorizeUser(idOperacion: 1)]
        [HttpGet]
        public ActionResult Add()
        {
            List<RolViewModel> lstRol = null;
            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                lstRol = (//from d in db.user
                          //join r in db.rol on d.idRol equals r.id
                          //select new RolViewModel
                          from d in db.rol
                          select new RolViewModel
                          {
                              Id = d.id,
                              Nombre = d.nombre
                          }).ToList();
            }

            List<SelectListItem> roles = lstRol.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.roles = roles;

            return View();
        }
        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        //FormCollection lee lo ingresado en el DropDonwList
        public ActionResult Add(UserViewModel model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int rol = Convert.ToInt32(collection["roles"]);

            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                user oUser = new user();
                oUser.idState = 1;
                oUser.documento = model.Documento;
                oUser.nombre = model.Nombre;
                oUser.apellido = model.Apellido;
                oUser.correo = model.Correo;
                oUser.idRol = rol;
                oUser.password = model.Password;

                db.user.Add(oUser);
                db.SaveChanges();
            }

            //NoseSiEstoVaya
            List<RolViewModel> lstRol = null;
            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                lstRol = (//from d in db.user
                          //join r in db.rol on d.idRol equals r.id
                          //select new RolViewModel
                          from d in db.rol
                          select new RolViewModel
                          {
                              Id = d.id,
                              Nombre = d.nombre
                          }).ToList();
            }
            List<SelectListItem> roles = lstRol.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre,
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
            ViewBag.roles = roles;
            

            @TempData["Message"] = "¡Usuario Creado Con Éxito!";
            return Redirect(Url.Content("~/User/"));
        }
        #endregion

        #region Edit
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Edit(int Id)
        {
            EditUserViewModel model = new EditUserViewModel();
            List<RolViewModel> lstRol = null;
            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                var oUser = db.user.Find(Id);
                model.Documento = oUser.documento;
                model.Nombre = oUser.nombre;
                model.Apellido = oUser.apellido;
                model.Correo = oUser.correo;
                model.IdRol = oUser.idRol;
                model.Password = oUser.password;

                lstRol = (from d in db.rol
                          select new RolViewModel
                          {
                              Id = d.id,
                              Nombre = d.nombre
                          }).ToList();
            }            

            List<SelectListItem> roles = lstRol.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString(),
                    Selected = true
                };
            });
            ViewBag.roles = roles;

            return View(model);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult Edit(EditUserViewModel model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int rol = Convert.ToInt32(collection["IdRol"]);

            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                var oUser = db.user.Find(model.Id);
                oUser.documento = model.Documento;
                oUser.nombre = model.Nombre;
                oUser.apellido = model.Apellido;
                oUser.correo = model.Correo;
                oUser.idRol = rol;

                if(model.Password != null && model.Password.Trim() != "")
                {
                    oUser.password = model.Password;
                }

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            @TempData["Message"] = "¡Usuario Editado Con Éxito!";
            return Redirect(Url.Content("~/User/"));
        }
        #endregion

        #region Delete
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                var oUser = db.user.Find(Id);
                oUser.idState = 3;
                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }
        #endregion

        #region buscador
        public ActionResult Buscador(string Nombre)
        {
            var db = new dbempresainvEntities();

            var busqueda = from i in db.inventario select i;

            if (!String.IsNullOrEmpty(Nombre))
            {
                busqueda = busqueda.Where(i => i.nombre.Contains(Nombre)
                                            || i.descripcion.Contains(Nombre));
            }
            return View(busqueda.ToList());
        }
        #endregion
    }
}