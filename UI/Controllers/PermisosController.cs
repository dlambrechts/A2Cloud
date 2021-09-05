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



        // GET: GrupoPermisos/Create
        public ActionResult CrearGrupo()
        {
            return View();
        }

        // POST: GrupoPermisos/Create
        [HttpPost]
        public ActionResult CrearGrupo(PerfilFamiliaBE fam)
        {
            try
            {
                perBLL.CrearFamilia(fam);

                return RedirectToAction("GrupoPermisos");
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
                              
               ViewBag.Familias = perBLL.ObtenerFamilias(); // Obtengo todos los grupos para poder agregar
              
               ViewBag.Permisos = perBLL.ObtenerPatentes(); // Obtengo todos los permisos individuales para poder agregar

                return View(sel);

            }

            else { return RedirectToAction("Index", "Login"); }
     
        }

        // POST: GrupoPermisos/Edit/5
        [HttpPost]
        public ActionResult EditarGrupo(PerfilFamiliaBE familia)
        {
            try
            {
                
                perBLL.EditarFamilia(familia);
                            
            
                return RedirectToAction("EditarGrupo","Permisos",familia.Id);
              
            }
            catch
            {
                return View("EditarGrupo", familia);
            }

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

        public JsonResult AgregarElemento(int Item,  string Tipo, int Fam)
        {
            try
            {
                PerfilFamiliaBE familia = new PerfilFamiliaBE();
                PerfilComponenteBE comp;

                familia.Id = Fam;
                familia = perBLL.ObtenerFamiliaPorId(familia);
                familia = perBLL.CompletarFamilia(familia);

                if (Tipo == "Grupo") 
                
                {
                    
                    comp = new PerfilFamiliaBE();
                    comp.Id = Item;

                    if(perBLL.VerificarPermisoExplisito(familia, comp))  // Esto evita que se genere una consulta recursiva al consultar el grupo despues de insertar
                    
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }                   

                }


                else { comp = new PerfilPatenteBE(); comp.Id = Item; }

                
                familia.AgregarHijo(comp);               

                perBLL.GuardarFamilia(familia);

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }





        // GET: Permisos/Delete/5
        public ActionResult Delete(int id)
        {
            PerfilFamiliaBE famDel = new PerfilFamiliaBE();
            famDel.Id = id;           
            return View(famDel);
        }

        // POST: Permisos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PerfilFamiliaBE famDel = new PerfilFamiliaBE();
                famDel.Id = id;
                perBLL.EliminarFamilia(famDel);


                return RedirectToAction("GrupoPermisos");
            }
            catch
            {
                return View();
            }
        }
    }
}
