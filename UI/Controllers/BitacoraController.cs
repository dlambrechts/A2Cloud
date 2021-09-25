using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;
using X.PagedList;

namespace UI.Controllers
{
    public class BitacoraController : Controller
    {

        BitacoraBLL bllBit = new BitacoraBLL();
        // GET: Bitacora
        public ActionResult Index(int? pagina,string FechaDesde, string FechaHasta)
        {
            if (Session["IdUsuario"] != null)
            {
                List<BitacoraBE> Registros = bllBit.ListarTodos();

                int RegistrosPorPagina = 15;
                int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;


                ViewBag.FechaDesde = FechaDesde;
                ViewBag.FechaHasta = FechaHasta;
                if (FechaDesde != null && FechaHasta != null)
                {
                    Registros = Registros.Where(reg => reg.FechaCreacion >= Convert.ToDateTime(FechaDesde)).ToList();
                    Registros = Registros.Where(reg => reg.FechaCreacion <= Convert.ToDateTime(FechaHasta)).ToList();
                }


                return View(Registros.ToPagedList(Indice, RegistrosPorPagina));
            }

            else { return RedirectToAction("Index", "Login"); }

        }
    

        // GET: Bitacora/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bitacora/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bitacora/Create
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

        // GET: Bitacora/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bitacora/Edit/5
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

        // GET: Bitacora/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bitacora/Delete/5
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
