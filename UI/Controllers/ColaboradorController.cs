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
    public class ColaboradorController : Controller
    {
        ColaboradorBLL bllCol = new ColaboradorBLL();
        DepartamentoBLL bllDep = new DepartamentoBLL();
        PerfilDeHardwareBLL bllPerf = new PerfilDeHardwareBLL();

        // GET: Colaborador
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<ColaboradorBE> Lista = new List<ColaboradorBE>();

            Lista = bllCol.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Nombre.ToUpper().Contains(Dato_Buscar.ToUpper())
                 || u.Departamento.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                 || u.Apellido.ToUpper().Contains(Dato_Buscar.ToUpper())
             ).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }

        // GET: Colaborador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Colaborador/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Departamentos"] = bllDep.Listar();
            ViewData["Perfiles"] = bllPerf.Listar();


            return View();
        }

        // POST: Colaborador/Create
        [HttpPost]
        public ActionResult Create(ColaboradorBE Colaborador)
        {
            try
            {
                ModelState.Remove("PerfilHardware.Descripcion");
                ModelState.Remove("Departamento.Descripcion");

                if (ModelState.IsValid)
                {
                    Colaborador.UsuarioCreacion = new UsuarioBE();
                    Colaborador.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                    bllCol.Insertar(Colaborador);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {
                    ViewData["Departamentos"] = bllDep.Listar();
                    ViewData["Perfiles"] = bllPerf.Listar();
                    return View("Create", Colaborador);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Colaborador/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ColaboradorBE Colaborador = new ColaboradorBE();
            Colaborador.Id = id;
            Colaborador = bllCol.ObtenerUno(Colaborador);

            ViewData["Departamentos"] = bllDep.Listar();
            ViewData["Perfiles"] = bllPerf.Listar();

            return View(Colaborador);
        }

        // POST: Colaborador/Edit/5
        [HttpPost]
        public ActionResult Edit(ColaboradorBE Colaborador)
        {
            try
            {
                ModelState.Remove("PerfilHardware.Descripcion");
                ModelState.Remove("Departamento.Descripcion");
                if (ModelState.IsValid)
                {
                    Colaborador.UsuarioModificacion = new UsuarioBE();
                    Colaborador.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllCol.Editar(Colaborador);
                }

                else

                {

                    ViewData["Departamentos"] = bllDep.Listar();
                    ViewData["Perfiles"] = bllPerf.Listar();

                    return View("Edit", Colaborador);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Colaborador/Delete/5
        public JsonResult Delete(int id)
        {
            try

            {
                ColaboradorBE Colaborador = new ColaboradorBE();
                Colaborador.Id = id;
                Colaborador.UsuarioModificacion = new UsuarioBE();
                Colaborador.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllCol.Eliminar(Colaborador);

                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }
    }
}
