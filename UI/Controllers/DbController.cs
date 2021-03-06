using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using BE;
using BLL;
using GestorDeArchivo;

namespace UI.Controllers
{
    public class DbController : Controller
    {
        BitacoraBLL bllBit = new BitacoraBLL();
        DigitoVerificadorBLL bllDv = new DigitoVerificadorBLL();
        BackupBLL bllBak = new BackupBLL();


        // GET: Integridad
        public ActionResult Integridad()
        {
            if (Session["IdUsuario"] != null)
            {
                return View();
            }
            else { return RedirectToAction("Index", "Login"); }
        }


        public JsonResult EjecutarAnalisis()
        {
            try
            {
                
      
                UsuarioBE user = new UsuarioBE();

                user.Id = Convert.ToInt32(Session["IdUsuario"]);
                user.Credencial.Mail = Session["Mail"].ToString();


                if (bllDv.VerificarIntegridad(user)) 
                
                {
                    return Json(new { success = true });
                }

                else return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                FileMananager.RegistrarError(ex.Message);
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: Backup
        public ActionResult Backup(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] != null)
            {
                try { 
                ViewBag.Resultado = TempData["Resultado"] as string;
                ViewBag.Fecha = TempData["FechaBack"] as string;

                List<BackupBE> lista = new List<BackupBE>();

                lista = bllBak.ListarBackups();


                if (Dato_Buscar != null)
                { pagina = 1; }
                else { Dato_Buscar = Valor_Filtro; }

                ViewBag.ValorFiltro = Dato_Buscar;

                if (!String.IsNullOrEmpty(Dato_Buscar))
                {
                    lista = lista.Where(u => u.Nombre.ToUpper().Contains(Dato_Buscar.ToUpper())
                    || u.FechaCreacion.ToString().ToUpper().Contains(Dato_Buscar.ToUpper())
                    ).ToList();
                }

                int RegistrosPorPagina = 10;
                int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
                return View(lista.ToPagedList(Indice, RegistrosPorPagina));

                } 
                catch (Exception ex) 
                { 
                    FileMananager.RegistrarError(ex.Message);
                    return RedirectToAction("Index", "Login");
                }

            }
            else { return RedirectToAction("Index", "Login"); }
        }


        // GET: Db/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] != null)
            {                
                
                return View();
            }

            else { return RedirectToAction("Index", "Login"); }
        }


        // POST: Db/Create
        [HttpPost]
        public ActionResult Create(BackupBE nuevoBackup)
        {
            try
            {             

                
                UsuarioBE user = new UsuarioBE();
                user.Id = Convert.ToInt32(Session["IdUsuario"]);
                user.Credencial.Mail = Session["Mail"].ToString();

                if (bllDv.VerificarIntegridad(user))
                {
                    bllBak.NuevoBackup(nuevoBackup);

                    TempData["Resultado"] = "Creado";

                    return RedirectToAction("Backup");
                }

                else 
                
                {
                    TempData["Resultado"] = "ErrorDv";

                    return RedirectToAction("Backup");
                }


            }
            catch
            {
                return RedirectToAction("Backup");
            }
        }



        // GET: Db/Restaurar/5
        public ActionResult Restaurar(string Nombre,DateTime Creacion)
        {
            BackupBE backRestore = new BackupBE();

            backRestore.Nombre = Nombre;
            backRestore.FechaCreacion = Creacion;

            return View(backRestore);
        }

        // POST: Db/Restaurar/5
        [HttpPost]
        public ActionResult Restaurar(string Nombre, DateTime Creacion,FormCollection form)
        {
            try
            {
                BackupBE backRestore = new BackupBE();

                backRestore.Nombre = Nombre;
                backRestore.FechaCreacion = Creacion;

                bllBak.RestaurarDb(backRestore);

                TempData["Resultado"] = "RestauradoOk";
                TempData["FechaBack"] = backRestore.FechaCreacion.ToString();

                return RedirectToAction("Backup");
            }
            catch
            {
                return RedirectToAction("Backup");
            }


        }

        // GET: Db/Restaurar/5
        public JsonResult Delete(string Nombre)
        {
            BackupBE backDel = new BackupBE();

            backDel.Nombre = Nombre;
            bllBak.EliminarBackup(backDel);

            return Json(new { success = true });

        }

    
    }
}
