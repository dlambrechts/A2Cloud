using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;

namespace UI.Controllers
{
    public class IdiomaController : Controller
    {
        // GET: Idioma
        public ActionResult Index()
        {

            return View();
        }

        // GET: Idioma/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Idioma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Idioma/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Idioma/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Idioma/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Idioma/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Idioma/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult ListaIdiomas()
        {
            IdiomaBLL bllIdioma = new IdiomaBLL();
            var lista = bllIdioma.ObtenerIdiomas();
    
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
