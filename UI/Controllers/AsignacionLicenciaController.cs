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
    public class AsignacionLicenciaController : Controller
    {
        AsignacionLicenciaBLL bllAsignacion = new AsignacionLicenciaBLL();
        ActivoBLL bllActivo = new ActivoBLL();
        ColaboradorBLL bllColaborador = new ColaboradorBLL();
        LicenciaBLL bllLicencia = new LicenciaBLL();
        AsignacionActivoBLL bllAsignacionActivo = new AsignacionActivoBLL();

        // GET: AsignacionLicencia
        public ActionResult Index(int? pagina, string Colaborador, string Estado,string Dispositivo)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            List<ColaboradorBE> Colaboradores = bllColaborador.Listar();
            ColaboradorBE defecto = new ColaboradorBE(); defecto.Id = 0; defecto.Nombre = "Todos los Colaboradores";
            Colaboradores.Add(defecto);
            Colaboradores = Colaboradores.OrderBy(x => x.Id).ToList();
            ViewBag.Colaboradores = Colaboradores;

            List<ActivoBE> Dispositivos = bllActivo.Listar();
            Dispositivos = Dispositivos.Where(x => x.Tipo.ArquitecturaPc == true).ToList();
            ActivoBE DispDefecto = new ActivoBE(); DispDefecto.Id = 0; DispDefecto.Nombre = "Todos los Dispositivos";
            Dispositivos.Add(DispDefecto);
            Dispositivos = Dispositivos.OrderBy(x => x.Id).ToList();
            ViewBag.Dispositivos = Dispositivos;

            List<AsignacionEstadoBE> Estados = bllAsignacionActivo.ListarEstados();
            AsignacionEstadoBE estDef = new AsignacionEstadoBE(); estDef.Id = 0; estDef.Descripcion = "Todos lo Estados";
            Estados.Add(estDef);
            Estados = Estados.OrderBy(x => x.Id).ToList();
            ViewBag.Estados = Estados;

            ViewBag.FinalizadoOk = TempData["FinalizadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<AsignacionLicenciaBE> Lista = new List<AsignacionLicenciaBE>();

            Lista = bllAsignacion.Listar();

            ViewBag.Colaborador = Colaborador;
            ViewBag.Estados = Estados;
            ViewBag.Dispositivos = Dispositivos;



            if (!String.IsNullOrEmpty(Colaborador) && Convert.ToInt32(Colaborador) != 0)

            {
                Lista = Lista.Where(reg => reg.Colaborador.Id == Convert.ToInt32(Colaborador)).ToList();
            }

            if (!String.IsNullOrEmpty(Estado) && Convert.ToInt32(Estado) != 0)

            {
                Lista = Lista.Where(reg => reg.Estado.Id == Convert.ToInt32(Estado)).ToList();
            }

            if (!String.IsNullOrEmpty(Dispositivo) && Convert.ToInt32(Dispositivo) != 0)

            {
                Lista = Lista.Where(reg => reg.Activo.Id == Convert.ToInt32(Dispositivo)).ToList();
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

        public void ExportarExcel()
        {
            try
            {
                if (Session["IdUsuario"] == null) RedirectToAction("Login", "Home");




                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("AsignacionLicenicas");

                List<AsignacionLicenciaBE> Asignaciones = new List<AsignacionLicenciaBE>();

                Asignaciones = bllAsignacion.Listar();

                workSheet.Cells[1, 1].LoadFromCollection(Asignaciones.Select(x => new { x.Detalle, Estado = x.Estado.Descripcion, Dispositivo = x.Activo.Nombre,  Colaborador = x.Colaborador.NombreCompleto, Creado = x.FechaCreacion, Modificado = x.FechaModificacion }).ToList(), true);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                workSheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#99C"));
                workSheet.Cells["A1:F1"].Style.Font.Size = 13;
                workSheet.Cells["A1:F1"].Style.Font.Name = "Calibri";
                workSheet.Cells["E2:E1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                workSheet.Cells["F2:F1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;


                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    Response.AddHeader("content-disposition", "attachment;  filename=AsignacionLicencias.xlsx");
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
