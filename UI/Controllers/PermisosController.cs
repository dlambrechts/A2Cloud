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
                // Obtengo los datos completos de la famila que voy a editar
                PerfilFamiliaBE sel = new PerfilFamiliaBE();
                sel.Id = id;
                sel = perBLL.ObtenerFamiliaPorId(sel);
                sel = perBLL.CompletarFamilia(sel);

                // Obtengo todos los grupos para poder agregar
           

               ViewBag.Familias = perBLL.ObtenerFamilias();

                // Obtengo todos los permisos individuales para poder agregar


                ViewBag.Permisos = perBLL.ObtenerPatentes();

                return View(sel);

            }

            else { return RedirectToAction("Index", "Login"); }
     
        }


     
        public JsonResult QuitarElemento(int Item, string Tipo, int Fam )
        {
            try
            {
                PerfilFamiliaBE familia = new PerfilFamiliaBE();
                PerfilComponenteBE comp;

                familia.Id = Fam;
                familia = perBLL.ObtenerFamiliaPorId(familia);
                familia = perBLL.CompletarFamilia(familia);

                if (Tipo=="Ninguno") { comp = new PerfilFamiliaBE(); }
                else { comp = new PerfilPatenteBE(); }

                comp.Id = Item;
                familia.QuitarHijo(comp);

                perBLL.GuardarFamilia(familia);

                return Json(new { status = "Success" }); 
            }
            catch
            {
                return Json(new { status = "Error" });
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
