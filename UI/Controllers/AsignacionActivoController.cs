using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BLL;
using X.PagedList;
using GestorDeArchivo;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;

namespace UI.Controllers
{
    public class AsignacionActivoController : Controller
    {
        ActivoBLL bllActivo = new ActivoBLL();
        ColaboradorBLL bllColaborador = new ColaboradorBLL();
        AsignacionActivoBLL bllAsignacionActivo = new AsignacionActivoBLL();

        // GET: AsignacionActivo
        public ActionResult Index(int? pagina, string Colaborador, string Estado)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            List<ColaboradorBE> Colaboradores = bllColaborador.Listar();
            ColaboradorBE defecto = new ColaboradorBE(); defecto.Id = 0; defecto.Nombre = "Todos los Colaboradores";
            Colaboradores.Add(defecto);
            Colaboradores = Colaboradores.OrderBy(x => x.Id).ToList();
            ViewBag.Colaboradores = Colaboradores;

            List<AsignacionEstadoBE> Estados = bllAsignacionActivo.ListarEstados();
            AsignacionEstadoBE estDef = new AsignacionEstadoBE(); estDef.Id = 0; estDef.Descripcion = "Todos lo Estados";
            Estados.Add(estDef);
            Estados = Estados.OrderBy(x => x.Id).ToList();
            ViewBag.Estados = Estados;

            ViewBag.FinalizadoOk = TempData["FinalizadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<AsignacionActivoBE> Lista = new List<AsignacionActivoBE>();

            Lista = bllAsignacionActivo.Listar();


            ViewBag.Colaborador = Colaborador;
            ViewBag.Estado = Estado;



            if (!String.IsNullOrEmpty(Colaborador) && Convert.ToInt32(Colaborador) != 0)

            {
                Lista = Lista.Where(reg => reg.Colaborador.Id == Convert.ToInt32(Colaborador)).ToList();
            }

            if (!String.IsNullOrEmpty(Estado) && Convert.ToInt32(Estado) != 0)

            {
                Lista = Lista.Where(reg => reg.Estado.Id == Convert.ToInt32(Estado)).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }

        // GET: AsignacionActivo/Details/5
        public ActionResult Details(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            AsignacionActivoBE AsignacionActivo = new AsignacionActivoBE();
            AsignacionActivo.Id = id;
            AsignacionActivo = bllAsignacionActivo.ObtenerUno(AsignacionActivo);
            return View(AsignacionActivo);
        }

        // GET: AsignacionActivo/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Activos"] = bllActivo.Listar().Where(x => x.Estado.Asignar() == true);
            ViewData["TiposAsignacion"] = bllAsignacionActivo.ListarTipoAsignacion();


            var Colaboradores = bllColaborador.Listar().Select(c => new { Id = c.Id, Descripcion = c.Nombre + " " + c.Apellido + " (" + c.Departamento.Descripcion + ")" }).ToList();
            ViewBag.Colaboradores = new SelectList(Colaboradores, "Id", "Descripcion");

            return View();
        }

        // POST: AsignacionActivo/Create
        [HttpPost]
        public ActionResult Create(AsignacionActivoBE Asignacion)
        {
            try
            {

                ColaboradorBE col = new ColaboradorBE();
                col.Id = Asignacion.Colaborador.Id;
                col = bllColaborador.ObtenerUno(col);

                ModelState.Remove("Marca.Descripcion");
                ModelState.Remove("Colaborador.Nombre");
                ModelState.Remove("Colaborador.Apellido");
                ModelState.Remove("Colaborador.Mail");
                ModelState.Remove("Activo.Nombre");
                ModelState.Remove("Activo.CicloDeVida");
                ModelState.Remove("Activo.Modelo");
                ModelState.Remove("Activo.ModeloProcesador");
                ModelState.Remove("Activo.NumeroSerie");

                if (Asignacion.Tipo.Id == 0 && col.FullRemoto == false) { ModelState.AddModelError(string.Empty, "Debe seleccionar la Ubicación"); }
                if (Asignacion.Colaborador.Id == 0) { ModelState.AddModelError(string.Empty, "Debe seleccionar un Colaborador"); }

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

                    var Colaboradores = bllColaborador.Listar().Select(c => new { Id = c.Id, Descripcion = c.Nombre + " " + c.Apellido + " (" + c.Departamento.Descripcion + ")" }).ToList();
                    ViewBag.Colaboradores = new SelectList(Colaboradores, "Id", "Descripcion");

                    return View("Create", Asignacion);
                }
            }
            catch (Exception ex)
            {
                FileMananager.RegistrarError(ex.Message);
                return View();
            }
        }


        [HttpPost]
        public JsonResult ObtenerColaborador(int Id)
        {
            ColaboradorBE colaborador = new ColaboradorBE();

            colaborador.Id = Id;
            colaborador = bllColaborador.ObtenerUno(colaborador);

            return Json(colaborador);
        }


        // GET: AsignacionActivo/Finalizar/5
        public ActionResult Finalizar(int id)
        {

            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            AsignacionActivoBE Asignacion = new AsignacionActivoBE();
            Asignacion.Id = id;
            Asignacion = bllAsignacionActivo.ObtenerUno(Asignacion);

            Asignacion.FechaFinalizacion = DateTime.Now;  // para que la fecha propuesta sea la fecha actual

            return View(Asignacion);
        }

        // POST: AsignacionActivo/finalizar/5
        [HttpPost]
        public ActionResult Finalizar(AsignacionActivoBE Asignacion)
        {
            try
            {

                ModelState.Clear();
                if (Asignacion.FechaFinalizacion < Asignacion.FechaInicio) { ModelState.AddModelError(string.Empty, "La fecha de finalización no puede ser menor a la fecha de asignación"); }

                if (ModelState.IsValid)

                {
                    Asignacion.UsuarioModificacion = new UsuarioBE();
                    Asignacion.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    bllAsignacionActivo.Finalizar(Asignacion);
                    TempData["FinalizadoOk"] = "Finalizado";

                    return RedirectToAction("Index");

                }

                else

                {
                    Asignacion = bllAsignacionActivo.ObtenerUno(Asignacion);

                    Asignacion.FechaFinalizacion = DateTime.Now;  // para que la fecha propuesta sea la fecha actual
                    return View("Finalizar", Asignacion);

                }
            }
            catch
            {
                return View();
            }
        }

        public void ExportarExcel()
        {
            try
            {
                if (Session["IdUsuario"] == null) RedirectToAction("Login", "Home");




                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("AsignacionActivos");

                List<AsignacionActivoBE> Asignaciones = new List<AsignacionActivoBE>();

                Asignaciones = bllAsignacionActivo.Listar();

                workSheet.Cells[1, 1].LoadFromCollection(Asignaciones.Select(x => new { x.Detalle, Dispositivo = x.Activo.Nombre, NúmeroSerie= x.Activo.NumeroSerie, Estado = x.Estado.Descripcion, Colaborador = x.Colaborador.NombreCompleto,  Ubicación = x.Tipo.Descripcion, Creado = x.FechaCreacion, Modificado = x.FechaModificacion }).ToList(), true);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                workSheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#99C"));
                workSheet.Cells["A1:H1"].Style.Font.Size = 13;
                workSheet.Cells["A1:H1"].Style.Font.Name = "Calibri";
                workSheet.Cells["G2:G1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                workSheet.Cells["H2:H1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;


                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    Response.AddHeader("content-disposition", "attachment;  filename=AsignacionDeActivos.xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();

                }
            }

            catch (Exception ex)
            {
                FileMananager.RegistrarError("Error al Exportar a XLS :" + ex.Message);

            }
        }
    }
}
