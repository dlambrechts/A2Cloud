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
                sel = perBLL.ObtenerFamiliaPorId(sel);

                sel = perBLL.CompletarFamilia(sel);
  
                return View(sel);

            }

            else { return RedirectToAction("Index", "Login"); }
     
        }


 
        public ActionResult QuitarElemento(int item, int fam, bool grupo)
        {
            try
            {
                // TODO: Add update logic here
                PerfilFamiliaBE familia = new PerfilFamiliaBE();
                PerfilComponenteBE comp;

                familia.Id = fam;
                familia = perBLL.ObtenerFamiliaPorId(familia);
                familia = perBLL.CompletarFamilia(familia);

                if (grupo == true) { comp = new PerfilFamiliaBE(); }
                else { comp = new PerfilPatenteBE(); }

                comp.Id = item;

                familia.QuitarHijo(comp);

                perBLL.GuardarFamilia(familia);


                return RedirectToAction("EditarGrupo", new { id = familia.Id });
            }
            catch
            {
                return View("GrupoPermisos");
            }
        }


        public ActionResult EditarGrupoParcial()
        {
            return PartialView();
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
