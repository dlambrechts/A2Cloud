using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using BLL;
using BE;
using GestorDeArchivo;


namespace UI.Controllers
{
    public class PermisosController : Controller
    {
        PerfilBLL perBLL = new PerfilBLL();
        UsuarioBLL usBLL = new UsuarioBLL();

        // GET: Permisos
        public ActionResult Permisos(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] != null)
            {

                List<PerfilPatenteBE> lista = new List<PerfilPatenteBE>(perBLL.ObtenerPatentes());
                if (Dato_Buscar != null)
                { pagina = 1; }
                else { Dato_Buscar = Valor_Filtro; }

                ViewBag.ValorFiltro = Dato_Buscar;

                if (!String.IsNullOrEmpty(Dato_Buscar))
                {
                    lista = lista.Where(u => u.Permiso.ToUpper().Contains(Dato_Buscar.ToUpper())
                         || u.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())                       
                        ).ToList();
                }

                int RegistrosPorPagina = 10;
                int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
                return View(lista.ToPagedList(Indice, RegistrosPorPagina));
                            
            }

            else { return RedirectToAction("Index", "Login"); }
        }

        public ActionResult GrupoPermisos(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] != null)
            {
                ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
                ViewBag.EliminadoNoOk = TempData["EliminadoNoOk"] as string;
                ViewBag.CreadoOk = TempData["CreadoOk"] as string;

                try {
                    List<PerfilFamiliaBE> lista = perBLL.ObtenerFamilias() as List<PerfilFamiliaBE>;

                    if (Dato_Buscar != null)
                    { pagina = 1; }
                    else { Dato_Buscar = Valor_Filtro; }

                    ViewBag.ValorFiltro = Dato_Buscar;

                    if (!String.IsNullOrEmpty(Dato_Buscar))
                    {
                        lista = lista.Where(u => u.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())).ToList();
                    }

                    int RegistrosPorPagina = 10;
                    int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

                    return View(lista.ToPagedList(Indice, RegistrosPorPagina));

                } catch (Exception ex) 
                
                {
                    FileMananager.RegistrarError(ex.Message);
                    return RedirectToAction("Index", "Login");
                    
                }
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
                fam.UsuarioCreacion = new UsuarioBE();
                fam.UsuarioCreacion.Id=Convert.ToInt32(Session["IdUsuario"]);
                perBLL.CrearFamilia(fam);
                TempData["CreadoOk"] = "Creado";
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

                IList<PerfilFamiliaBE> familias = perBLL.ObtenerFamilias(); // Obtengo todos los grupos para poder agregar desde el select
                familias = familias.Where(item => item.Id != sel.Id).ToList(); // Quitar el grupo actual de la lista
                ViewBag.Familias = familias;

                IList<PerfilPatenteBE> permisos = perBLL.ObtenerPatentes(); // Obtengo todos los permisos individuales para poder agregar


                ViewBag.Permisos = permisos; 

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
                familia.UsuarioModificacion = new UsuarioBE();
                familia.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);
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

                if (Tipo=="") { comp = new PerfilFamiliaBE(); }
                else { comp = new PerfilPatenteBE(); }

                comp.Id = Item;
                familia.QuitarHijo(comp);

                familia.UsuarioModificacion = new UsuarioBE();
                familia.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);
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

                    if(perBLL.VerificarPermisoImplicito(familia, comp))  // Detectar permisos explísitos que generen errores de recursividad
                    
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }                   

                }


                else { comp = new PerfilPatenteBE(); comp.Id = Item; }

                
                familia.AgregarHijo(comp);

                familia.UsuarioModificacion = new UsuarioBE();
                familia.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);
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
            if(Session["IdUsuario"]==null) return RedirectToAction("Index", "Login");


            PerfilFamiliaBE famDel = new PerfilFamiliaBE();
            famDel.Id = id;
            famDel = perBLL.ObtenerFamiliaPorId(famDel);

            if (perBLL.FamiliaVerificarUso(famDel) == true)
            {
                TempData["EliminadoNoOk"] = "NoEliminado";
                return RedirectToAction("GrupoPermisos");

            }

            else { return View(famDel); }
            
        }

        // POST: Permisos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PerfilFamiliaBE famDel = new PerfilFamiliaBE();
                famDel.Id = id;
                famDel.UsuarioModificacion = new UsuarioBE();
                famDel.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                perBLL.EliminarFamilia(famDel);
                TempData["EliminadoOk"] = "Eliminado";

                return RedirectToAction("GrupoPermisos");
            }
            catch 
            {
                return View();
            }
        }

        // GET: GrupoPermisos/EditarPerfilUsuario/5
        public ActionResult EditarPerfilUsuario(int id)
        {
            if (Session["IdUsuario"] != null)
            {
                // Obtengo los datos completos del usuario
                UsuarioBE usuario = new UsuarioBE();
                usuario.Id = id;
                usuario=usBLL.ObtenerUno(usuario);

                perBLL.CargarPerfilUsuario(usuario);



                IList<PerfilFamiliaBE> familias = perBLL.ObtenerFamilias(); // Obtengo todos los grupos para poder agregar desde el select
                ViewBag.Familias = familias;

                IList<PerfilPatenteBE> permisos = perBLL.ObtenerPatentes(); // Obtengo todos los permisos individuales para poder agregar
                ViewBag.Permisos = permisos;

                return View(usuario);

            }

            else { return RedirectToAction("Index", "Login"); }

        }

        public JsonResult AgregarElementoAlPerfil(int Item, string Tipo, int Usuario)
        {
            try
            {
                UsuarioBE usuario = new UsuarioBE();
                PerfilComponenteBE comp;

                usuario.Id = Usuario;
                usuario = usBLL.ObtenerUno(usuario);

                perBLL.CargarPerfilUsuario(usuario);

                if (Tipo == "Grupo")

                {
                    comp = new PerfilFamiliaBE();
                    comp.Id = Item;
                }


                else { comp = new PerfilPatenteBE(); comp.Id = Item; }


                usuario.AgregarPermiso(comp);

                usBLL.GuardarPerfil(usuario);

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult QuitarElementoDelPerfil(int Item, string Tipo, int Usuario)
        {
            try
            {
                UsuarioBE usuario = new UsuarioBE();
                PerfilComponenteBE comp;

                usuario.Id = Usuario;
                usuario = usBLL.ObtenerUno(usuario);

                perBLL.CargarPerfilUsuario(usuario);

                if (Tipo == "Ninguno") { comp = new PerfilFamiliaBE(); }
                else { comp = new PerfilPatenteBE(); }

                comp.Id = Item;
                usuario.QuitarPermiso(comp);

                usBLL.GuardarPerfil(usuario);

                return Json(new { status = "Success" });
            }
            catch
            {
                return Json(new { status = "Error" });
            }
        }
    }
}
