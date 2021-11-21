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
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            AsignacionLicenciaBE AsignacionLicencia = new AsignacionLicenciaBE();
            AsignacionLicencia.Id = id;
            AsignacionLicencia = bllAsignacion.ObtenerUno(AsignacionLicencia);
            return View(AsignacionLicencia);
        }

        // GET: AsignacionLicencia/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Activos"] = bllActivo.Listar();

            ViewData["Licencias"] = bllLicencia.Listar().Where(x=>x.Disponible>0).ToList();          

            var Colaboradores = bllColaborador.Listar().Select(c => new { Id = c.Id, Descripcion = c.Nombre + " " + c.Apellido + " (" + c.Departamento.Descripcion + ")" }).ToList();
            ViewBag.Colaboradores = new SelectList(Colaboradores, "Id", "Descripcion");

            return View();
        }

        // POST: AsignacionLicencia/Create
        [HttpPost]
        public ActionResult Create(AsignacionLicenciaBE Asignacion)
        {
            try
            {
                Asignacion.Licencia = bllLicencia.ObtenerUno(Asignacion.Licencia);

                ModelState.Remove("Licencia.Descripcion");
                ModelState.Remove("Colaborador.Nombre");
                ModelState.Remove("Colaborador.Apellido");
                ModelState.Remove("Colaborador.Mail");
                ModelState.Remove("Activo.Nombre");
                ModelState.Remove("Activo.CicloDeVida");
                ModelState.Remove("Activo.Modelo");
                ModelState.Remove("Activo.ModeloProcesador");
                ModelState.Remove("Activo.NumeroSerie");

                if(Asignacion.Licencia.Modalidad.Id.Equals("PerpetuaDispositivo") || Asignacion.Licencia.Modalidad.Id.Equals("SuscripcionDispositivo")) 
                
                {
                    Asignacion.Colaborador.Id=0;
                
                }

                else { Asignacion.Activo.Id = 0; }

                if (ModelState.IsValid)
                {
                    Asignacion.UsuarioCreacion = new UsuarioBE();
                    Asignacion.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    Asignacion.Colaborador = bllColaborador.ObtenerUno(Asignacion.Colaborador);

                    bllAsignacion.Insertar(Asignacion);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {

                    ViewData["Activos"] = bllActivo.Listar();

                    ViewData["Licencias"] = bllLicencia.Listar().Where(x => x.Disponible > 0).ToList();

                    var Colaboradores = bllColaborador.Listar().Select(c => new { Id = c.Id, Descripcion = c.Nombre + " " + c.Apellido + " (" + c.Departamento.Descripcion + ")" }).ToList();
                    ViewBag.Colaboradores = new SelectList(Colaboradores, "Id", "Descripcion");

                    return View("Create", Asignacion);
                }
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

        // GET: AsignacionActivo/Finalizar/5
        public ActionResult Finalizar(int id)
        {

            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            AsignacionLicenciaBE Asignacion = new AsignacionLicenciaBE();
            Asignacion.Id = id;
            Asignacion = bllAsignacion.ObtenerUno(Asignacion);

            Asignacion.FechaFinalizacion = DateTime.Now;  // para que la fecha propuesta sea la fecha actual

            return View(Asignacion);
        }

        // POST: AsignacionActivo/finalizar/5
        [HttpPost]
        public ActionResult Finalizar(AsignacionLicenciaBE Asignacion)
        {
            try
            {

                ModelState.Clear();
                if (Asignacion.FechaFinalizacion < Asignacion.FechaInicio) { ModelState.AddModelError(string.Empty, "La fecha de finalización no puede ser menor a la fecha de asignación"); }

                if (ModelState.IsValid)

                {
                    Asignacion.UsuarioModificacion = new UsuarioBE();
                    Asignacion.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    bllAsignacion.Finalizar(Asignacion);
                    TempData["FinalizadoOk"] = "Finalizado";

                    return RedirectToAction("Index");

                }

                else

                {
                    Asignacion = bllAsignacion.ObtenerUno(Asignacion);

                    Asignacion.FechaFinalizacion = DateTime.Now;  // para que la fecha propuesta sea la fecha actual
                    return View("Finalizar", Asignacion);

                }
            }
            catch
            {
                return View();
            }
        }
    }
}
