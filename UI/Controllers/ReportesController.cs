using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iText;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using BLL;
using BE;
using iText.Commons.Actions;
using iText.Kernel.Events;
using iText.Layout.Borders;
using iText.IO.Font.Constants;

using iText.Layout;
using iText.Kernel.Pdf.Canvas;

namespace UI.Controllers
{
    public class ReportesController : Controller
    {
        AsignacionActivoBLL bllAsignacion = new AsignacionActivoBLL();
        ColaboradorBLL bllColaborador = new ColaboradorBLL();
        UsuarioBLL bllUsuario = new UsuarioBLL();
        ActivoBLL bllActivo = new ActivoBLL();
        AsignacionLicenciaBLL bllAsignacionLic = new AsignacionLicenciaBLL();

        // GET: ReporteAsignacionActivos
        public ActionResult ReporteAsignacionActivos()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            List<ColaboradorBE> Colaboradores = bllColaborador.Listar();
            ColaboradorBE defecto = new ColaboradorBE(); defecto.Id = 0; defecto.Nombre = "Todos los Colaboradores";
            Colaboradores.Add(defecto);
            Colaboradores = Colaboradores.OrderBy(x => x.Id).ToList();
            ViewBag.Colaboradores = Colaboradores;

            List<AsignacionEstadoBE> Estados = bllAsignacion.ListarEstados();
            AsignacionEstadoBE estDef = new AsignacionEstadoBE(); estDef.Id = 0; estDef.Descripcion = "Todos lo Estados";
            Estados.Add(estDef);
            Estados = Estados.OrderBy(x => x.Id).ToList();
            ViewBag.Estados = Estados;

            ViewBag.ErrorFechas = TempData["ErrorFechas"] as string;
            return View();
        }


        [HttpPost]
        public ActionResult ReporteAsignacionActivos(string Colaborador, string Estado, string Desde, string Hasta)

        {
            UsuarioBE emisor = new UsuarioBE();
            emisor.Id = Convert.ToInt32(Session["IdUsuario"]);
            emisor = bllUsuario.ObtenerUno(emisor);

            DateTime desde = Convert.ToDateTime(Desde);
            DateTime hasta = Convert.ToDateTime(Hasta);

            if (desde > hasta)
            {
                TempData["ErrorFechas"] = "Error";

                return RedirectToAction("ReporteAsignacionActivos");

            }

            List<AsignacionActivoBE> Asignaciones = new List<AsignacionActivoBE>(bllAsignacion.Listar());

            if (!Colaborador.Equals("0")) {   Asignaciones = Asignaciones.Where(x => x.Colaborador.Id == Convert.ToInt32(Colaborador)).ToList();}

            if (!Estado.Equals("0")) { Asignaciones = Asignaciones.Where(x => x.Estado.Id == Convert.ToInt32(Estado)).ToList(); }

            Asignaciones = Asignaciones.Where(x => x.FechaInicio > desde && x.FechaInicio < hasta).ToList();

            MemoryStream ms = new MemoryStream();
            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.A4);
            doc.SetMargins(75, 35, 70, 35);

            string pathLogo = Server.MapPath("~/Content/img/Logo.png");
            Image img = new Image(iText.IO.Image.ImageDataFactory.Create(pathLogo));
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1(img,emisor)); // Carga el encabezado
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler1()); // Carga el pié de página

            Table tabla = new Table(1).UseAllAvailableWidth();

            Cell celda = new Cell().Add(new Paragraph("Reporte de Asignación de Activos").SetFontSize(14))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tabla.AddCell(celda);

            doc.Add(tabla);

            Style estiloCelda = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            Table tabla2 = new Table(5).UseAllAvailableWidth();
            Cell celda2 = new Cell(2, 1).Add(new Paragraph("Nombre"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell(2, 1).Add(new Paragraph("Número de Serie"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Fecha de Asignación"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Estado de la Asignación"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Colaborador"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));

            


            foreach (AsignacionActivoBE item in Asignaciones)
            {

                celda2 = new Cell().Add(new Paragraph(item.Activo.Nombre));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Activo.NumeroSerie));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.FechaInicio.ToShortDateString()));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Estado.Descripcion));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Colaborador.NombreCompleto));
                tabla2.AddCell(celda2);
            }

            doc.Add(tabla2);

            doc.Close();

            byte[] bytesStraam = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStraam, 0, bytesStraam.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }
  
    

    // GET: ReporteCicloDeVidaActivos
    public ActionResult ReporteCicloDeVidaActivos()
    {
        if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            List<ActivoEstadoBE> Estados = bllActivo.Estados();
            ActivoEstadoDisponibleBE defecto = new ActivoEstadoDisponibleBE(); defecto.Codigo = "0"; defecto.Descripcion = "Todos los Estados";
            Estados.Add(defecto);
            Estados = Estados.OrderBy(x => x.Codigo).ToList();
            ViewBag.Estados = Estados;

            List<ActivoTipoBE> Tipos = bllActivo.ListarTipos();
            ActivoTipoBE Tipodefecto = new ActivoTipoBE(); Tipodefecto.Id = 0; Tipodefecto.Descripcion = "Todos lo Tipos";
            Tipos.Add(Tipodefecto);
            Tipos = Tipos.OrderBy(x => x.Id).ToList();
            ViewBag.Tipos = Tipos;

            return View();
    }

     [HttpPost]
     public ActionResult ReporteCicloDeVidaActivos(string Tipo, string Estado, string Desde, string Hasta)

        {
            UsuarioBE emisor = new UsuarioBE();
            emisor.Id = Convert.ToInt32(Session["IdUsuario"]);
            emisor = bllUsuario.ObtenerUno(emisor);

            DateTime desde = Convert.ToDateTime(Desde);
            DateTime hasta = Convert.ToDateTime(Hasta);

            if (desde > hasta)
            {
                TempData["ErrorFechas"] = "Error";

                return RedirectToAction("ReporteAsignacionActivos");

            }

            List<ActivoBE> Activos = new List<ActivoBE>(bllActivo.Listar());

            if (!Tipo.Equals("0")) { Activos = Activos.Where(x => x.Tipo.Id == Convert.ToInt32(Tipo)).ToList(); }

            if (!Estado.Equals("0")) { Activos = Activos.Where(x => x.Estado.Codigo.Equals(Estado)).ToList(); }

            Activos = Activos.Where(x => x.FinCicloDeVida > desde && x.FinCicloDeVida < hasta).ToList();

            MemoryStream ms = new MemoryStream();
            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.A4);
            doc.SetMargins(75, 35, 70, 35);

            string pathLogo = Server.MapPath("~/Content/img/Logo.png");
            Image img = new Image(iText.IO.Image.ImageDataFactory.Create(pathLogo));
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1(img, emisor)); // Carga el encabezado
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler1()); // Carga el pié de página

            Table tabla = new Table(1).UseAllAvailableWidth();

            Cell celda = new Cell().Add(new Paragraph("Reporte de Ciclo de Vide de Activos").SetFontSize(14))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tabla.AddCell(celda);

            doc.Add(tabla);

            Style estiloCelda = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            Table tabla2 = new Table(7).UseAllAvailableWidth();
            Cell celda2 = new Cell(2, 1).Add(new Paragraph("Nombre"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell(2, 1).Add(new Paragraph("Número de Serie"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Tipo"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Estado"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Fecha Adquisición"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Ciclo de Vida"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Vida Útil Restante"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));




            foreach (ActivoBE item in Activos)
            {

                celda2 = new Cell().Add(new Paragraph(item.Nombre));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.NumeroSerie));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Tipo.Descripcion));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Estado.Descripcion));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.FechaCompra.ToShortDateString()));
                tabla2.AddCell(celda2);

                string a;
                if (item.CicloDeVida == 1) { a = " Año"; } else { a = " Años"; }

                celda2 = new Cell().Add(new Paragraph(item.CicloDeVida.ToString() + a));
                tabla2.AddCell(celda2);

                int DiasRestantes= (int)item.FinCicloDeVida.Subtract(DateTime.Now).TotalDays;

                celda2 = new Cell().Add(new Paragraph(Convert.ToString(DiasRestantes + " Días")));
                tabla2.AddCell(celda2);
            }

            doc.Add(tabla2);

            doc.Close();

            byte[] bytesStraam = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStraam, 0, bytesStraam.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");


        }



        public ActionResult ReporteAsignacionLicencias()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            List<ColaboradorBE> Colaboradores = bllColaborador.Listar();
            ColaboradorBE defecto = new ColaboradorBE(); defecto.Id = 0; defecto.Nombre = "Todos los Colaboradores";
            Colaboradores.Add(defecto);
            Colaboradores = Colaboradores.OrderBy(x => x.Id).ToList();
            ViewBag.Colaboradores = Colaboradores;

            List<AsignacionEstadoBE> Estados = bllAsignacion.ListarEstados();
            AsignacionEstadoBE estDef = new AsignacionEstadoBE(); estDef.Id = 0; estDef.Descripcion = "Todos lo Estados";
            Estados.Add(estDef);
            Estados = Estados.OrderBy(x => x.Id).ToList();
            ViewBag.Estados = Estados;

            List<ActivoBE> Activos = bllActivo.Listar();
            ActivoBE ActDef = new ActivoBE(); ActDef.Id = 0; ActDef.Nombre = "Todos lo Dispositivos";
            Activos.Add(ActDef);
            Activos = Activos.OrderBy(x => x.Id).ToList();
            ViewBag.Activos = Activos;



            ViewBag.ErrorFechas = TempData["ErrorFechas"] as string;
            return View();
        }

        [HttpPost]
        public ActionResult ReporteAsignacionLicencias(string Colaborador, string Estado, string Dispositivo, string Desde, string Hasta)

        {
            UsuarioBE emisor = new UsuarioBE();
            emisor.Id = Convert.ToInt32(Session["IdUsuario"]);
            emisor = bllUsuario.ObtenerUno(emisor);

            DateTime desde = Convert.ToDateTime(Desde);
            DateTime hasta = Convert.ToDateTime(Hasta);

            if (desde > hasta)
            {
                TempData["ErrorFechas"] = "Error";

                return RedirectToAction("ReporteAsignacionActivos");

            }

            List<AsignacionLicenciaBE> Asignaciones = new List<AsignacionLicenciaBE>(bllAsignacionLic.Listar());

            if (!Colaborador.Equals("0")) { Asignaciones = Asignaciones.Where(x => x.Colaborador.Id == Convert.ToInt32(Colaborador)).ToList(); }

            if (!Estado.Equals("0")) { Asignaciones = Asignaciones.Where(x => x.Estado.Id == Convert.ToInt32(Estado)).ToList(); }

            if (!Dispositivo.Equals("0")) { Asignaciones = Asignaciones.Where(x => x.Activo.Id == Convert.ToInt32(Dispositivo)).ToList(); }

            Asignaciones = Asignaciones.Where(x => x.FechaInicio > desde && x.FechaInicio < hasta).ToList();

            MemoryStream ms = new MemoryStream();
            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.A4);
            doc.SetMargins(75, 35, 70, 35);

            string pathLogo = Server.MapPath("~/Content/img/Logo.png");
            Image img = new Image(iText.IO.Image.ImageDataFactory.Create(pathLogo));
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1(img, emisor)); // Carga el encabezado
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler1()); // Carga el pié de página

            Table tabla = new Table(1).UseAllAvailableWidth();

            Cell celda = new Cell().Add(new Paragraph("Reporte de Asignación de Licencias").SetFontSize(14))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tabla.AddCell(celda);

            doc.Add(tabla);

            Style estiloCelda = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            Table tabla2 = new Table(5).UseAllAvailableWidth();
            Cell celda2 = new Cell(2, 1).Add(new Paragraph("Licencia"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell(2, 1).Add(new Paragraph("Número de Contrato"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Fecha de Asignación"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Estado de la Asignación"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            //celda2 = new Cell().Add(new Paragraph("Tipo de Asignación"));
            //tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Asignado A"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));





            foreach (AsignacionLicenciaBE item in Asignaciones)
            {

                celda2 = new Cell().Add(new Paragraph(item.Licencia.Descripcion));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Licencia.NumeroContrato));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.FechaInicio.ToShortDateString()));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Estado.Descripcion));
                tabla2.AddCell(celda2);
                //celda2 = new Cell().Add(new Paragraph(item.Licencia.Modalidad.Descripcion));
                //tabla2.AddCell(celda2);

                string Asignado = "";
                if (item.Licencia.Modalidad.Id.Contains("Dispositivo")) { Asignado = item.Activo.Nombre; } else { Asignado = item.Colaborador.NombreCompleto; }

                celda2 = new Cell().Add(new Paragraph(Asignado));
                tabla2.AddCell(celda2);
            }

            doc.Add(tabla2);

            doc.Close();

            byte[] bytesStraam = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStraam, 0, bytesStraam.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        public class HeaderEventHandler1 : iText.Kernel.Events.IEventHandler // Para encabezado 

    {
        Image Img;
        UsuarioBE Emisor;
        public HeaderEventHandler1(Image img,UsuarioBE emisor)
        {
            Img = img;
            Emisor = emisor;
        }
    public void HandleEvent(Event @event) 
        {


            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage pagina = docEvent.GetPage();

      
            Rectangle rootArea = new Rectangle(35, pagina.GetPageSize().GetTop() - 75, pagina.GetPageSize().GetRight() - 70, 55);
            Canvas canvas = new Canvas(pagina, rootArea);
            canvas.Add(getTable(docEvent));
          
        }

        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 20f, 80f };
            Table tableEvent = new Table(iText.Layout.Properties.UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            Style styleCell = new Style()
                 .SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFontSize(10f);

            Cell cell = new Cell().Add(Img.SetAutoScale(true)).SetBorder(new SolidBorder(ColorConstants.BLACK,1));

            tableEvent.AddCell(cell.AddStyle(styleCell).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT));

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            cell = new Cell()

                .Add(new Paragraph("Emitido Por: "+ Emisor.Nombre +" "+Emisor.Apellido +" \n").SetFont(bold))
                .Add(new Paragraph("Fecha de Emisión: " + DateTime.Now.ToShortDateString()))
                .AddStyle(styleText).AddStyle(styleCell)
                .SetBorder(new SolidBorder(ColorConstants.BLACK,1));
                ;
                
            tableEvent.AddCell(cell);



            return tableEvent;
        }


    }

    public class FooterEventHandler1 : iText.Kernel.Events.IEventHandler // Para pié de página
    {
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage pagina = docEvent.GetPage();


            Rectangle rootArea = new Rectangle(36,20, pagina.GetPageSize().GetRight() - 70, 55);
            Canvas canvas = new Canvas(pagina, rootArea);
            canvas.Add(getTable(docEvent));
        }

        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 92f, 8f }; // % de primera y segunda celda
            Table tableEvent = new Table(iText.Layout.Properties.UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            PdfPage page = docEvent.GetPage();
            int pageNum = docEvent.GetDocument().GetPageNumber(page);

            Style styleCell = new Style()
            .SetPadding(5)
            .SetBorder(Border.NO_BORDER)
            .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 2));

            Cell cell = new Cell().Add(new Paragraph("A2Cloud"));

            tableEvent.AddCell(cell
                .AddStyle(styleCell)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontColor(ColorConstants.LIGHT_GRAY));
            cell = new Cell().Add(new Paragraph(pageNum.ToString()));
            cell.AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.BLACK)
                .SetFontColor(ColorConstants.WHITE)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);


            tableEvent.AddCell(cell);



            return tableEvent;
        }
    }
 }
}