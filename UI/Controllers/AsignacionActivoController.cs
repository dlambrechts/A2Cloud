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
    public class AsignacionActivoController : Controller
    {
        ActivoBLL bllActivo = new ActivoBLL();
        ColaboradorBLL bllColaborador = new ColaboradorBLL();
        AsignacionActivoBLL bllAsignacionActivo = new AsignacionActivoBLL();

        // GET: AsignacionActivo
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<AsignacionActivoBE> Lista = new List<AsignacionActivoBE>();

            Lista = bllAsignacionActivo.Listar();

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

        // GET: AsignacionActivo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AsignacionActivo/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Activos"] = bllActivo.Listar().Where(x=>x.Estado.Asignar()==true);
            ViewData["TiposAsignacion"] = bllAsignacionActivo.ListarTipoAsignacion();

            var Colaboradores= bllColaborador.Listar().Select(c =>new { Id=c.Id,Descripcion=c.Nombre + " " + c.Apellido}).ToList();
            ViewBag.Colaboradores = new SelectList(Colaboradores, "Id", "Descripcion");

            return View();
        }

        // POST: AsignacionActivo/Create
        [HttpPost]
        public ActionResult Create(AsignacionActivoBE Asignacion)
        {
            try
            {
                ModelState.Remove("Marca.Descripcion");
                ModelState.Remove("Colaborador.Nombre");
                ModelState.Remove("Colaborador.Apellido");
                ModelState.Remove("Colaborador.Mail");
                ModelState.Remove("Activo.Nombre");
                ModelState.Remove("Activo.CicloDeVida");
                ModelState.Remove("Activo.Modelo");
                ModelState.Remove("Activo.ModeloProcesador");

                if (ModelState.IsValid)
                {
                    Asignacion.UsuarioCreacion = new UsuarioBE();
                    Asignacion.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    Asignacion.Colaborador = bllColaborador.ObtenerUno(Asignacion.Colaborador);

                    bllAsignacionActivo.Insertar(Asignacion);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {
                    ViewData["Activos"] = bllActivo.Listar().Where(x => x.Estado.Asignar() == true);
                    ViewData["TiposAsignacion"] = bllAsignacionActivo.ListarTipoAsignacion();

                    var Colaboradores = bllColaborador.Listar().Select(c => new { Id = c.Id, Descripcion = c.Nombre + " " + c.Apellido }).ToList();
                    ViewBag.Colaboradores = new SelectList(Colaboradores, "Id", "Descripcion");

                    return View("Create", Asignacion);
                }
            }
            catch(Exception ex)
            {
                FileMananager.RegistrarError(ex.Message);
                return View();
            }
        }

        // GET: AsignacionActivo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AsignacionActivo/Edit/5
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

        // GET: AsignacionActivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AsignacionActivo/Delete/5
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
