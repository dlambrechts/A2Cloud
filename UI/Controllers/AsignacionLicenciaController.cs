using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class AsignacionLicenciaController : Controller
    {
        // GET: AsignacionLicencia
        public ActionResult Index()
        {
            return View();
        }

        // GET: AsignacionLicencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AsignacionLicencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AsignacionLicencia/Create
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

        // GET: AsignacionLicencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AsignacionLicencia/Edit/5
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

        // GET: AsignacionLicencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AsignacionLicencia/Delete/5
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
