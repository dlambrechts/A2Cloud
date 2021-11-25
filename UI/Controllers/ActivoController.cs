using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;
using X.PagedList;
using GestorDeArchivo;

namespace UI.Controllers
{
    public class ActivoController : Controller
    {
        ActivoBLL bllActivo = new ActivoBLL();
        MarcaBLL bllMarca = new MarcaBLL();


        // GET: Activo
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro, string Tipo, string Estado) 
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<ActivoEstadoBE> Estados = bllActivo.Estados();
            ActivoEstadoDisponibleBE defecto = new ActivoEstadoDisponibleBE(); defecto.Codigo = "0"; defecto.Descripcion = "Todos los Estados";
            Estados.Add(defecto);
            Estados = Estados.OrderBy(x => x.Codigo).ToList();
            ViewBag.Estados = Estados;

            List<ActivoTipoBE> Tipos = bllActivo.ListarTipos();
            ActivoTipoBE Tipodefecto = new ActivoTipoBE(); Tipodefecto.Id = 0; Tipodefecto.Descripcion = "Todos lo Tipos";
            Tipos.Add(Tipodefecto);
            Tipos = Tipos.OrderBy(x => x.Id).ToList();
            ViewBag.Tipos = Tipos;


            List<ActivoBE> Lista = new List<ActivoBE>();

            Lista = bllActivo.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;
            ViewBag.Estado = Estado;
            ViewBag.Tipo = Tipo;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Nombre.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Marca.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Modelo.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Marca.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                    ).ToList();
            }

            if (!String.IsNullOrEmpty(Estado) && !Estado.Equals("0"))

            {
                Lista = Lista.Where(reg => reg.Estado.Codigo == Estado).ToList();
            }
            if (!String.IsNullOrEmpty(Tipo) && Convert.ToInt32(Tipo) != 0)

            {
                Lista = Lista.Where(reg => reg.Tipo.Id == Convert.ToInt32(Tipo)).ToList();
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
            catch (Exception ex)
            {
                FileMananager.RegistrarError(ex.Message);
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

        // GET: Activo/Baja/5
        public JsonResult Baja(int id)
        {
            try

            {
                ActivoBE Activo = new ActivoBE();
                Activo.Id = id;
                Activo.UsuarioModificacion = new UsuarioBE();
                Activo.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);
                ActivoEstadoBajaBE nuevoEstado = new ActivoEstadoBajaBE();
                Activo.CambiarEstado(nuevoEstado);
                bllActivo.CambiarEstado(Activo);

                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }


    }
}
