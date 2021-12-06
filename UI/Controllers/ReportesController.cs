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
        
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reportes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reportes/Create
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

        // GET: Reportes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reportes/Edit/5
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

        // GET: Reportes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reportes/Delete/5
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

        public ActionResult Pdf() 
        
        {
            MemoryStream ms = new MemoryStream();
            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.A4);
            doc.SetMargins(75, 35, 70, 35);

            string pathLogo = Server.MapPath("~/Content/img/Logo.png");
            Image img = new Image(iText.IO.Image.ImageDataFactory.Create(pathLogo));
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1(img)); // Carga el encabezado
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler1()); // Carga el pié de página

            

            Table tabla = new Table(1).UseAllAvailableWidth();

            Cell celda = new Cell().Add(new Paragraph("Reporte de Asignación de Activos").SetFontSize(14))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tabla.AddCell(celda);

            celda = new Cell().Add(new Paragraph("lalala"))
                 .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            tabla.AddCell(celda);

            doc.Add(tabla);

            Style estiloCelda = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            Table tabla2 = new Table(4).UseAllAvailableWidth();
            Cell celda2 = new Cell(2, 1).Add(new Paragraph("Nombre"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell(2, 1).Add(new Paragraph("Numero Serie"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Fecha de Asignación"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));
            celda2 = new Cell().Add(new Paragraph("Colaborador"));
            tabla2.AddHeaderCell(celda2.AddStyle(estiloCelda));

            List<AsignacionActivoBE> Asignaciones = new List<AsignacionActivoBE>(bllAsignacion.Listar());


            foreach (AsignacionActivoBE item in Asignaciones)
            {

                celda2 = new Cell().Add(new Paragraph(item.Activo.Nombre));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.Activo.NumeroSerie));
                tabla2.AddCell(celda2);
                celda2 = new Cell().Add(new Paragraph(item.FechaInicio.ToShortDateString()));
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
  
    }

    public class HeaderEventHandler1 : iText.Kernel.Events.IEventHandler // Para encabezado 

    {
        Image Img;
        public HeaderEventHandler1(Image img)
        {
            Img = img;
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

                .Add(new Paragraph("Emitido Por: Pepe\n").SetFont(bold))
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

            Cell cell = new Cell().Add(new Paragraph(DateTime.Now.ToLongDateString()));

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
