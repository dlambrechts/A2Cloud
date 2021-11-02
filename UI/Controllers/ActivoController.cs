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
    public class ActivoController : Controller
    {
        ActivoBLL bllActivo = new ActivoBLL();
        MarcaBLL bllMarca = new MarcaBLL();


        // GET: Activo
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro) 
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<ActivoBE> Lista = new List<ActivoBE>();

            Lista = bllActivo.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Nombre.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Marca.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Modelo.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Marca.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                    ).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }

        // GET: Activo/Details/5
        public ActionResult Details(int id)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ActivoBE Activo = new ActivoBE();
            Activo.Id = id;
            Activo = bllActivo.ObtenerPorId(Activo);
            return View(Activo);
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
                Activo.Tipo = bllActivo.ObtenerTipoPorId(Activo.Tipo);
                if (Activo.Tipo.ArquitecturaPc == false) { ModelState.Remove("ModeloProcesador"); }

                ModelState.Remove("Marca.Descripcion");
                if (ModelState.IsValid)
                {
                    Activo.UsuarioCreacion = new UsuarioBE();
                    Activo.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);
                    

                    bllActivo.Insertar(Activo);
                    TempData["CreadoOk"] = "Creado";

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

            ViewData["Marcas"] = bllMarca.Listar();
            ViewData["Tipos"] = bllActivo.ListarTipos();
            return View(Activo);
        }

        // POST: Activo/Edit/5
        [HttpPost]
        public ActionResult Edit(ActivoBE Activo)
        {
            try
            {
                Activo.Tipo = bllActivo.ObtenerTipoPorId(Activo.Tipo);
                if (Activo.Tipo.ArquitecturaPc == false) { ModelState.Remove("ModeloProcesador"); }

                ModelState.Remove("Marca.Descripcion");

                if (ModelState.IsValid)
                {
                    Activo.UsuarioModificacion = new UsuarioBE();
                    Activo.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllActivo.Editar(Activo);
                }

                else 
                
                {
                    ViewData["Marcas"] = bllMarca.Listar();
                    ViewData["Tipos"] = bllActivo.ListarTipos();
                    return View("Edit",Activo);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Activo/Delete/5
        public JsonResult Delete(int id)
        {
            try 
            
            {

                ActivoBE Activo = new ActivoBE();
                Activo.Id = id;
                Activo.UsuarioModificacion = new UsuarioBE();
                Activo.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllActivo.Eliminar(Activo);

                return Json(new { success = true });

            }

            catch 
            
            {
                return Json(new { success = false });
            }
        }


    }
}
