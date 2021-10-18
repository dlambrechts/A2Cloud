using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using BLL;
using BE;

namespace UI.Controllers
{
    public class ProductoSoftwareController : Controller
    {
        ProductoSoftwareBLL bllSoft = new ProductoSoftwareBLL();
        MarcaBLL bllMarca = new MarcaBLL();
        // GET: ProductoSoftware
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewBag.EliminadoOk = TempData["EliminadoOk"] as string;
            ViewBag.EditadoOk = TempData["EditadoOk"] as string;
            ViewBag.CreadoOk = TempData["CreadoOk"] as string;

            List<ProductoSoftwareBE> Lista = new List<ProductoSoftwareBE>();

            Lista = bllSoft.Listar();

            if (Dato_Buscar != null)
            { pagina = 1; }
            else { Dato_Buscar = Valor_Filtro; }

            ViewBag.ValorFiltro = Dato_Buscar;

            if (!String.IsNullOrEmpty(Dato_Buscar))
            {
                Lista = Lista.Where(u => u.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                     || u.Marca.Descripcion.ToUpper().Contains(Dato_Buscar.ToUpper())
                    ).ToList();
            }

            int RegistrosPorPagina = 10;
            int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

            return View(Lista.ToPagedList(Indice, RegistrosPorPagina));
        }
            // GET: ProductoSoftware/Details/5
            public ActionResult Details(int id)

        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ProductoSoftwareBE soft = new ProductoSoftwareBE();
            soft.Id = id;
            soft=bllSoft.ObtenerUno(soft);

            return View(soft);
        }

        // GET: ProductoSoftware/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] == null) { return RedirectToAction("Index", "Login"); }

            ViewData["Marcas"] = bllMarca.Listar();
            return View();
        }

        // POST: ProductoSoftware/Create
        [HttpPost]
        public ActionResult Create(ProductoSoftwareBE Soft)
        {
            try
            {
                ModelState.Remove("Marca.Descripcion");
                if (ModelState.IsValid)
                {
                    Soft.UsuarioCreacion = new UsuarioBE();
                    Soft.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);


                    bllSoft.Insertar(Soft);
                    TempData["CreadoOk"] = "Creado";

                    return RedirectToAction("Index");
                }

                else
                {
                    ViewData["Marcas"] = bllMarca.Listar();

                    return View("Create", Soft);
                }
            }
            catch
            {
                return View();
            }
        }


        // GET: ProductoSoftware/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["IdUsuario"] == null) return RedirectToAction("Index", "Login");
            ProductoSoftwareBE Soft = new ProductoSoftwareBE();
            Soft.Id = id;
            Soft = bllSoft.ObtenerUno(Soft);

            ViewData["Marcas"] = bllMarca.Listar();

            return View(Soft);
        }

        // POST: ProductoSoftware/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductoSoftwareBE Soft)
        {
            try
            {
      
              
                ModelState.Remove("Marca.Descripcion");

                if (ModelState.IsValid)
                {
                    Soft.UsuarioModificacion = new UsuarioBE();
                    Soft.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                    TempData["EditadoOk"] = "Editado";
                    bllSoft.Editar(Soft);
                }

                else

                {
                    ViewData["Marcas"] = bllMarca.Listar();
                    return View("Edit", Soft);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: ProductoSoftware/Delete/5
        public JsonResult Delete(int id)
        {
            try

            {
                ProductoSoftwareBE Soft = new ProductoSoftwareBE();
                Soft.Id = id;
                Soft.UsuarioModificacion = new UsuarioBE();
                Soft.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                bllSoft.Eliminar(Soft);
                return Json(new { success = true });

            }

            catch

            {
                return Json(new { success = false });
            }
        }
    }
}
