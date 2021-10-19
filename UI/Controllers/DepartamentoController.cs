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
    public class DepartamentoController : Controller
    {
        DepartamentoBLL bllDepartamento = new DepartamentoBLL();

        // GET: Departamento
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<DepartamentoBE> Lista = new List<DepartamentoBE>();

            Lista = bllDepartamento.Listar();

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

        // GET: Departamento/Details/5
        public ActionResult Details(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            DepartamentoBE Departamento = new DepartamentoBE();
            Departamento.Id = id;
            Departamento = bllDepartamento.ObtenerUno(Departamento);
            return View(Departamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            return View();
        }

        // POST: Departamento/Create
        [HttpPost]
        public ActionResult Create(DepartamentoBE Departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Departamento.UsuarioCreacion = new UsuarioBE();
                    Departamento.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                    bllDepartamento.Insertar(Departamento);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {

                    return View("Create", Departamento);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Departamento/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            DepartamentoBE Departamento = new DepartamentoBE();
            Departamento.Id = id;
            Departamento = bllDepartamento.ObtenerUno(Departamento);

            return View(Departamento);
        }

        // POST: Departamento/Edit/5
        [HttpPost]
        public ActionResult Edit(DepartamentoBE Departamento)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Departamento.UsuarioModificacion = new UsuarioBE();
                    Departamento.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllDepartamento.Editar(Departamento);
                }

                else

                {
                    return View("Edit", Departamento);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Departamento/Delete/5
        public JsonResult Delete(int id)
        {
            try

            {
                DepartamentoBE Departamento = new DepartamentoBE();
                Departamento.Id = id;
                Departamento.UsuarioModificacion = new UsuarioBE();
                Departamento.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllDepartamento.Eliminar(Departamento);

                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }
    }
}
