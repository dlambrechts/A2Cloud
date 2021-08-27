using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BLL;

namespace UI.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            UsuarioBLL bllUsuario = new UsuarioBLL();
            var lista=bllUsuario.ListarTodos();
            return View(lista);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            IdiomaBLL bllIdioma= new IdiomaBLL();
            ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas();
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(UsuarioBE usuario)
        {
            try
            {
               if (ModelState.IsValid)
              {
                    UsuarioBLL bllU = new UsuarioBLL();
                    bllU.Insertar(usuario);
                    return RedirectToAction("Index");
            
             }
                else {
                    IdiomaBLL bllIdioma = new IdiomaBLL();
                    ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas();
                    return View("Create", usuario);

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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
    }
}
