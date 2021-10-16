using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;

namespace UI.Controllers
{
    public class ActivoController : Controller
    {
        ActivoBLL bllActivo = new ActivoBLL();
        MarcaBLL bllMarca = new MarcaBLL();


        // GET: Activo
        public ActionResult Index() 
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            List<ActivoBE> Lista = new List<ActivoBE>();

            Lista = bllActivo.Listar();

            return View(Lista);
        }

        // GET: Activo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Activo/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Marcas"] = bllMarca.Listar();
            ViewData["Tipos"] = bllActivo.ListarTipos();



            return View();
        }

        // POST: Activo/Create
        [HttpPost]
        public ActionResult Create(ActivoBE Activo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Activo.UsuarioCreacion = new UsuarioBE();
                    Activo.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);
                    Activo.Tipo = bllActivo.ObtenerTipoPorId(Activo.Tipo);

                    bllActivo.Insertar(Activo);

                    return RedirectToAction("Index");
                }

                else
                {
                    ViewData["Marcas"] = bllMarca.Listar();
                    ViewData["Tipos"] = bllActivo.ListarTipos();
                    return View("Create", Activo); 
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Activo/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) return RedirectToAction("Index", "Login");
            ActivoBE Activo = new ActivoBE();
            Activo.Id = id;
            Activo = bllActivo.ObtenerPorId(Activo);
           

            return View(Activo);
        }

        // POST: Activo/Edit/5
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

        // GET: Activo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Activo/Delete/5
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
