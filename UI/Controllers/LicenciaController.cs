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
    public class LicenciaController : Controller
    {
        LicenciaBLL bllLic = new LicenciaBLL();
        ProductoSoftwareBLL bllProd = new ProductoSoftwareBLL();
        // GET: Licencia
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<LicenciaBE> Lista = new List<LicenciaBE>();

            Lista = bllLic.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }

        // GET: Licencia/Details/5
        public ActionResult Details(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            LicenciaBE Licencia = new LicenciaBE();
            Licencia.Id = id;
            Licencia = bllLic.ObtenerUno(Licencia);
            return View(Licencia);
        }

        // GET: Licencia/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Modalidades"] = bllLic.ListarModalidades();
            ViewData["Productos"] = bllProd.Listar();
            return View();
        }

        // POST: Licencia/Create
        [HttpPost]
        public ActionResult Create(LicenciaBE Licencia)
        {
            try
            {
                ModelState.Remove("Producto.Descripcion");
                if (ModelState.IsValid)
                {
                    Licencia.UsuarioCreacion = new UsuarioBE();
                    Licencia.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                    bllLic.Insertar(Licencia);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {
                    ViewData["Modalidades"] = bllLic.ListarModalidades();
                    ViewData["Productos"] = bllProd.Listar();
                    return View("Create", Licencia);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Licencia/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) return RedirectToAction("Index", "Login");
            LicenciaBE Licencia = new LicenciaBE();
            Licencia.Id = id;
            Licencia = bllLic.ObtenerUno(Licencia);

            ViewData["Modalidades"] = bllLic.ListarModalidades();
            ViewData["Productos"] = bllProd.Listar();
            return View(Licencia);
        }

        // POST: Licencia/Edit/5
        [HttpPost]
        public ActionResult Edit(LicenciaBE Licencia)
        {
            try
            {
                ModelState.Remove("Producto.Descripcion");
                if (ModelState.IsValid)
                {
                    Licencia.UsuarioModificacion = new UsuarioBE();
                    Licencia.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllLic.Editar(Licencia);
                }

                else

                {
                    ViewData["Modalidades"] = bllLic.ListarModalidades();
                    ViewData["Productos"] = bllProd.Listar();
                    return View("Edit", Licencia);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Licencia/Delete/5
        public JsonResult Delete(int id)
        {
            try

            {

                LicenciaBE Licencia = new LicenciaBE();
                Licencia.Id = id;
                Licencia.UsuarioModificacion = new UsuarioBE();
                Licencia.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllLic.Eliminar(Licencia);

                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }

        public void ExportarExcel()
        {
            try
            {
                if (Session["IdUsuario"] == null) RedirectToAction("Login", "Home");




                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Licencias");

                List<LicenciaBE> Licencias = new List<LicenciaBE>();

                Licencias = bllLic.Listar();

                workSheet.Cells[1, 1].LoadFromCollection(Licencias.Select(x => new { Descripción = x.Descripcion,Producto=x.Producto.Descripcion,x.Cantidad, x.FechaVigencia, x.FechaFinalizacion, Creado = x.FechaCreacion, Modificado = x.FechaModificacion }).ToList(), true);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                workSheet.Cells["A1:G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#99C"));
                workSheet.Cells["A1:G1"].Style.Font.Size = 13;
                workSheet.Cells["A1:G1"].Style.Font.Name = "Calibri";
                workSheet.Cells["D2:D1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                workSheet.Cells["E2:E1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                workSheet.Cells["F2:F1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                workSheet.Cells["G2:G1000"].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;




                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    Response.AddHeader("content-disposition", "attachment;  filename=Licencias.xlsx");
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
