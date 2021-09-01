using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;

namespace UI.Controllers
{
    public class PermisosController : Controller
    {
        PerfilBLL perBLL = new PerfilBLL();

        // GET: Permisos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GrupoPermisos()
        {
            if (Session["IdUsuario"] != null)
            {
                var lista=perBLL.ObtenerFamilias();
                return View(lista);
            }

            else { return RedirectToAction("Index", "Login"); }
        }

        // GET: Permisos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GrupoPermisos/Create
        public ActionResult CrearGrupo()
        {
            return View();
        }

        // POST: GrupoPermisos/Create
        [HttpPost]
        public ActionResult CrearGrupo(FormCollection collection)
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

        // GET: GrupoPermisos/Edit/5
        public ActionResult EditarGrupo(int id)
        {
            if (Session["IdUsuario"] != null)
            {
                PerfilFamiliaBE sel = new PerfilFamiliaBE();
                sel.Id = id;

                IList<PerfilComponenteBE> flia = null;

  
                    flia = perBLL.ObtenerTodo(sel);  // Traer de la DB
                    foreach (var i in flia)
                        sel.AgregarHijo(i);
  
                return View(sel);

            }

            else { return RedirectToAction("Index", "Login"); }
     
        }

        // POST: GrupoPermisos/Edit/5
        [HttpPost]
        public ActionResult EditarGrupo(int id, FormCollection collection)
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

        // GET: Permisos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Permisos/Delete/5
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
