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
    public class LocalizacionController : Controller
    {
        LocalizacionBLL bllLoc = new LocalizacionBLL();

        // GET: Localizacion
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<LocalizacionBE> Lista = new List<LocalizacionBE>();

            Lista = bllLoc.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())

             ).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }

        // GET: Localizacion/Details/5
        public ActionResult Details(int id)
        {
            
                if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

                LocalizacionBE Localizacion = new LocalizacionBE();
                Localizacion.Id = id;
                Localizacion = bllLoc.ObtenerUno(Localizacion);
                return View(Localizacion);
            
        }

        // GET: Localizacion/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            return View();
        }

        // POST: Localizacion/Create
        [HttpPost]
        public ActionResult Create(LocalizacionBE Localizacion)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    Localizacion.UsuarioCreacion = new UsuarioBE();
                    Localizacion.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                    bllLoc.Insertar(Localizacion);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {

                    return View("Create", Localizacion);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Localizacion/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            LocalizacionBE Localizacion = new LocalizacionBE();
            Localizacion.Id = id;
            Localizacion = bllLoc.ObtenerUno(Localizacion);


            return View(Localizacion);
        }

        // POST: Localizacion/Edit/5
        [HttpPost]
        public ActionResult Edit(LocalizacionBE Localizacion)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Localizacion.UsuarioModificacion = new UsuarioBE();
                    Localizacion.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllLoc.Editar(Localizacion);
                }

                else

                {

                    return View("Edit", Localizacion);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Localizacion/Delete/5
        public JsonResult Delete(int id)
        {
            try

            {
                LocalizacionBE Localizacion = new LocalizacionBE();
                Localizacion.Id = id;
                Localizacion.UsuarioModificacion = new UsuarioBE();
                Localizacion.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllLoc.Eliminar(Localizacion);

                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }
    }
}
