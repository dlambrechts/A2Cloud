using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using BLL;
using BE;

namespace UI.Controllers
{
    public class ProductoSoftwareController : Controller
    {
        ProductoSoftwareBLL bllSoft = new ProductoSoftwareBLL();
        // GET: ProductoSoftware
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<ProductoSoftwareBE> Lista = new List<ProductoSoftwareBE>();

            Lista = bllSoft.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Marca.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                    ).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }
            // GET: ProductoSoftware/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoSoftware/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoSoftware/Create
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

        // GET: ProductoSoftware/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoSoftware/Edit/5
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

        // GET: ProductoSoftware/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoSoftware/Delete/5
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
