using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventario.Models;
using PagedList;
using Inventario.Filters;
using Inventario.Models.ViewModels;

//Categoría de un producto
namespace Inventario.Controllers
{
    public class CategoriaController : Controller
    {
        #region Index
        [AuthorizeUser(idOperacion: 12)]
        // GET: Categoria
        public ActionResult Index(string search, int? i)
        {
            var db = new dbempresainvEntities();
            List<categoria_producto> lst = db.categoria_producto.ToList();

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View(db.categoria_producto.Where(c => c.nombre.StartsWith(search) || search == null && c.idState == 1).ToList().ToPagedList(i ?? 1, 5));
        }
        #endregion

        #region Add
        [AuthorizeUser(idOperacion: 9)]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [AuthorizeUser(idOperacion: 9)]
        [HttpPost]
        public ActionResult Add(CategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new dbempresainvEntities())
            {
                var oCategoria = new categoria_producto();
                oCategoria.idState = 1;
                oCategoria.nombre = model.Nombre;
                oCategoria.descripcion = model.Descripcion;
                db.categoria_producto.Add(oCategoria);
                db.SaveChanges();
            }
            @TempData["Message"] = "¡Categoría creada con éxito!";
            return Redirect(Url.Content("~/Categoria/"));
        }
        #endregion

        #region Edit
        [AuthorizeUser(idOperacion: 10)]
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoriaViewModel model = new EditCategoriaViewModel();
            using (var db = new dbempresainvEntities())
            {
                var oCategoria = db.categoria_producto.Find(Id);
                model.Id = oCategoria.id;
                model.Nombre = oCategoria.nombre;
                model.Descripcion = oCategoria.descripcion;
            }
            return View(model);
        }

        [AuthorizeUser(idOperacion: 10)]
        [HttpPost]
        public ActionResult Edit(EditCategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new dbempresainvEntities())
            {
                var oCategoria = db.categoria_producto.Find(model.Id);
                oCategoria.nombre = model.Nombre;
                oCategoria.descripcion = model.Descripcion;
                db.Entry(oCategoria).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            @TempData["Message"] = "¡Categoría editada con éxito!";
            return Redirect(Url.Content("~/Categoria/"));
        }
        #endregion

        #region Delete
        [HttpPost]
        [AuthorizeUser(idOperacion:11)]
        public ActionResult Delete(int Id)
        {
            using (var db=new dbempresainvEntities())
            {
                var oCategoria = db.categoria_producto.Find(Id);
                oCategoria.idState = 3;

                db.Entry(oCategoria).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Content("1");
        }
        #endregion
    }
}