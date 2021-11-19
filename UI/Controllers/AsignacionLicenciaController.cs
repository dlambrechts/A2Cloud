using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BLL;
using X.PagedList;
using GestorDeArchivo;

namespace UI.Controllers
{
    public class AsignacionLicenciaController : Controller
    {
        AsignacionLicenciaBLL bllAsignacion = new AsignacionLicenciaBLL();
        ActivoBLL bllActivo = new ActivoBLL();
        ColaboradorBLL bllColaborador = new ColaboradorBLL();
        LicenciaBLL bllLicencia = new LicenciaBLL();

        // GET: AsignacionLicencia
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }


            ViewBag.FinalizadoOk = TempData["FinalizadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<AsignacionLicenciaBE> Lista = new List<AsignacionLicenciaBE>();

            Lista = bllAsignacion.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Detalle.ToUpper().Contains(Dato_Buscar.ToUpper())).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }

        // GET: AsignacionLicencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AsignacionLicencia/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Activos"] = bllActivo.Listar();

            ViewData["Licencias"] = bllLicencia.Listar();

           

            var Colaboradores = bllColaborador.Listar().Select(c => new { Id = c.Id, Descripcion = c.Nombre + " " + c.Apellido + " (" + c.Departamento.Descripcion + ")" }).ToList();
            ViewBag.Colaboradores = new SelectList(Colaboradores, "Id", "Descripcion");

            return View();
        }

        // POST: AsignacionLicencia/Create
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

        [HttpPost]
        public JsonResult ObtenerLicencia(int Id)
        {
            LicenciaBE licencia = new LicenciaBE();

            licencia.Id = Id;
            licencia = bllLicencia.ObtenerUno(licencia);

            return Json(licencia);
        }

        // GET: AsignacionLicencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AsignacionLicencia/Delete/5
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
