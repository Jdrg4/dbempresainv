using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Inventario.Models;
using Inventario.Filters;
using Inventario.Models.ViewModels;
using PagedList;

namespace Inventario.Controllers
{
    public class InventarioController : Controller
    {
        #region Index
        [AuthorizeUser(idOperacion: 8)]
        public ActionResult Index(string search, int? i)
        {
            dbempresainvEntities db = new dbempresainvEntities();
            List<inventario> lstProduct = db.inventario.ToList();

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View(db.inventario.Where(x => x.nombre.StartsWith(search) || search == null && x.idState == 1).ToList().ToPagedList(i ?? 1, 5));
        }

        // GET: Inventario
        //[AuthorizeUser(idOperacion: 8)]
        //public ActionResult Index(string Nombre)
        //{                    
        //    InventarioTableViewModel inv = new InventarioTableViewModel();
        //    List<InventarioTableViewModel> lst = null;
        //    using (dbempresainvEntities db = new dbempresainvEntities())
        //    {
        //        lst = (from i in db.inventario
        //               join c in db.categoria_producto on i.idCategoria equals c.id
        //               where i.idState == 1
        //               orderby i.id
        //               select new InventarioTableViewModel
        //               {
        //                   Id = i.id,
        //                   Nombre = i.nombre,
        //                   Descripcion = i.descripcion,
        //                   IdCategoria = c.nombre,
        //                   Precio = (float)i.precio,
        //                   fecha = i.fecha
        //               }).ToList();
        //    }
        //    if (TempData["Message"] != null)
        //    {
        //        ViewBag.Message = TempData["Message"].ToString();
        //    }

        //    return View(lst);
        //}
        #endregion

        #region Add
        [AuthorizeUser(idOperacion: 5)]
        [HttpGet]
        public ActionResult Add()
        {
            List<CategoriaViewModel> lstCategoria = null;
            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                lstCategoria = (from c in db.categoria_producto
                                select new CategoriaViewModel
                                {
                                    Id = c.id,
                                    Nombre = c.nombre,
                                    Descripcion = c.descripcion
                                }).ToList();
                List<SelectListItem> categorias = lstCategoria.ConvertAll(c =>
                {
                    return new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nombre,
                        Selected = false

                    };
                });
                ViewBag.Categorias = categorias;
                return View();
            }
        }
        [AuthorizeUser(idOperacion: 5)]
        [HttpPost]
        public ActionResult Add(InventarioViewModel model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int categ = Convert.ToInt32(collection["IdCategoria"]);
            using (var db = new dbempresainvEntities())
            {
                inventario oInv = new inventario();
                oInv.idState = 1;
                oInv.nombre = model.Nombre;
                oInv.descripcion = model.Descripcion;
                oInv.precio = (decimal)model.Precio;
                oInv.fecha = DateTime.Now;
                oInv.idCategoria = categ;
                db.inventario.Add(oInv);
                db.SaveChanges();
            }
            @TempData["Message"] = "¡Producto Creado Con Éxito!";
            return Redirect(Url.Content("~/Inventario/"));
        }
        #endregion

        #region Edit
        [AuthorizeUser(idOperacion: 6)]
        public ActionResult Edit(int Id)
        {
            EditInventarioViewModel model = new EditInventarioViewModel();
            List<CategoriaViewModel> lstCateg = null;

            using (dbempresainvEntities db = new dbempresainvEntities())
            {
                var oInv = db.inventario.Find(Id);
                model.Nombre = oInv.nombre;
                model.Descripcion = oInv.descripcion;
                model.Precio = (float)oInv.precio;
                model.IdCategoria = oInv.idCategoria;
                lstCateg = (from c in db.categoria_producto
                            select new CategoriaViewModel
                            {
                                Id = c.id,
                                Nombre = c.nombre
                            }).ToList();
            }
            List<SelectListItem> categorias = lstCateg.ConvertAll(c =>
            {
                return new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre,
                    Selected = false
                };
            });
            ViewBag.Categorias = categorias;
            return View(model);
        }

        [AuthorizeUser(idOperacion: 6)]
        [HttpPost]
        public ActionResult Edit(EditInventarioViewModel model, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            int categoria = Convert.ToInt32(collection["IdCategoria"]);

            using (var db = new dbempresainvEntities())
            {
                var oInv = db.inventario.Find(model.Id);
                oInv.nombre = model.Nombre;
                oInv.descripcion = model.Descripcion;
                oInv.precio = (decimal)model.Precio;
                oInv.idCategoria = categoria;
                oInv.fecha = DateTime.Now;
                db.Entry(oInv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            @TempData["Message"] = "¡Producto Editado Con Exito!";
            return Redirect(Url.Content("~/Inventario/"));
        }
        #endregion

        #region Delete
        [AuthorizeUser(idOperacion: 7)]
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db = new dbempresainvEntities())
            {
                var oInv = db.inventario.Find(Id);
                oInv.idState = 3;
                db.Entry(oInv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }
        #endregion

    }
}