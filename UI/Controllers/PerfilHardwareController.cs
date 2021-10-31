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
    public class PerfilHardwareController : Controller
    {
        PerfilDeHardwareBLL bllPerfilDeHardware = new PerfilDeHardwareBLL();
        ActivoBLL bllActivo = new ActivoBLL();

        // GET: PerfilDeHardware
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<PerfilDeHardwareBE> Lista = new List<PerfilDeHardwareBE>();

            Lista = bllPerfilDeHardware.Listar();

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

        // GET: PerfilDeHardware/Details/5
        public ActionResult Details(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            PerfilDeHardwareBE PerfilDeHardware = new PerfilDeHardwareBE();
            PerfilDeHardware.Id = id;
            PerfilDeHardware = bllPerfilDeHardware.ObtenerUno(PerfilDeHardware);
            return View(PerfilDeHardware);
        }

        // GET: PerfilDeHardware/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Tipos"] = bllActivo.ListarTipos().Where(x=>x.ArquitecturaPc==true);
            return View();
        }

        // POST: PerfilDeHardware/Create
        [HttpPost]
        public ActionResult Create(PerfilDeHardwareBE PerfilDeHardware)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PerfilDeHardware.UsuarioCreacion = new UsuarioBE();
                    PerfilDeHardware.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                    bllPerfilDeHardware.Insertar(PerfilDeHardware);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {
                    ViewData["Tipos"] = bllActivo.ListarTipos().Where(x => x.ArquitecturaPc == true);
                    return View("Create", PerfilDeHardware);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PerfilDeHardware/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            PerfilDeHardwareBE PerfilDeHardware = new PerfilDeHardwareBE();
            PerfilDeHardware.Id = id;
            PerfilDeHardware = bllPerfilDeHardware.ObtenerUno(PerfilDeHardware);

            ViewData["Tipos"] = bllActivo.ListarTipos().Where(x => x.ArquitecturaPc == true);
            return View(PerfilDeHardware);
        }

        // POST: PerfilDeHardware/Edit/5
        [HttpPost]
        public ActionResult Edit(PerfilDeHardwareBE PerfilDeHardware)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    PerfilDeHardware.UsuarioModificacion = new UsuarioBE();
                    PerfilDeHardware.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllPerfilDeHardware.Editar(PerfilDeHardware);
                }

                else

                {
                    ViewData["Tipos"] = bllActivo.ListarTipos().Where(x => x.ArquitecturaPc == true);
                    return View("Edit", PerfilDeHardware);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PerfilDeHardware/Delete/5
        public JsonResult Delete(int id)
        {
            try

            {
                PerfilDeHardwareBE PerfilDeHardware = new PerfilDeHardwareBE();
                PerfilDeHardware.Id = id;
                PerfilDeHardware.UsuarioModificacion = new UsuarioBE();
                PerfilDeHardware.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllPerfilDeHardware.Eliminar(PerfilDeHardware);

                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }
    }
}
