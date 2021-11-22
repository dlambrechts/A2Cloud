using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BLL;
using X.PagedList;

namespace UI.Controllers
{
    public class InteligenciaController : Controller
    {
        InteligenciaBLL bllInteligencia = new InteligenciaBLL();
        // GET: Inteligencia
        public ActionResult AnalisisActivosOciosos(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }



            List<RecomendacionBE> Lista = new List<RecomendacionBE>();

            Lista = bllInteligencia.AnalisisActivosOciosos();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Hallazgo.ToUpper().Contains(Dato_Buscar.ToUpper())).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }

        // GET: Inteligencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inteligencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inteligencia/Create
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

        // GET: Inteligencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inteligencia/Edit/5
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

        // GET: Inteligencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inteligencia/Delete/5
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
