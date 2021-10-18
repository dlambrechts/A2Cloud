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
    public class MarcaController : Controller
    {
        MarcaBLL bllMarca = new MarcaBLL();

        // GET: Marca
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<MarcaBE> Lista = new List<MarcaBE>();

            Lista = bllMarca.Listar();

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

        // GET: Marca/Details/5
        public ActionResult Details(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            MarcaBE Marca = new MarcaBE();
            Marca.Id = id;
            Marca = bllMarca.ObtenerUno(Marca);
            return View(Marca);
        }

        // GET: Marca/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            return View();
        }

        // POST: Marca/Create
        [HttpPost]
        public ActionResult Create(MarcaBE Marca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Marca.UsuarioCreacion = new UsuarioBE();
                    Marca.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                    bllMarca.Insertar(Marca);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {

                    return View("Create", Marca);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Marca/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            MarcaBE Marca = new MarcaBE();
            Marca.Id = id;
            Marca = bllMarca.ObtenerUno(Marca);

            return View(Marca);
        }

        // POST: Marca/Edit/5
        [HttpPost]
        public ActionResult Edit(MarcaBE Marca)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Marca.UsuarioModificacion = new UsuarioBE();
                    Marca.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllMarca.Editar(Marca);
                }

                else

                {
                    return View("Edit", Marca);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Marca/Delete/5
        public JsonResult Delete(int id)
        {
            try

            {
                MarcaBE Marca = new MarcaBE();
                Marca.Id = id;
                Marca.UsuarioModificacion = new UsuarioBE();
                Marca.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllMarca.Eliminar(Marca);

                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }
    }
}
