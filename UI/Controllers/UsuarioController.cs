using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BLL;
using Seguridad;

namespace UI.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioBLL bllU = new UsuarioBLL();

        // GET: Usuario
        public ActionResult Index()
        {
            UsuarioBLL bllUsuario = new UsuarioBLL();
            var lista=bllUsuario.ListarTodos();
            ViewBag.Resultado = TempData["Resultado"] as string;
            return View(lista);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            IdiomaBLL bllIdioma= new IdiomaBLL();
            ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas();
            ViewBag.Resultado = TempData["Resultado"] as string;
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(UsuarioBE usuario)
        {
            try
            {

               if (ModelState.IsValid && bllU.ValidarExistencia(usuario.Credencial)==false)
                    {
                        usuario.Credencial.Contraseña = Encriptado.Hash(usuario.Credencial.Contraseña);
                        bllU.Insertar(usuario);
                        TempData["Resultado"] = "Creado";
                        return RedirectToAction("Index");
                    }

                        
                else {
                    IdiomaBLL bllIdioma = new IdiomaBLL();
                    ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas();
                    if (bllU.ValidarExistencia(usuario.Credencial) == true) { TempData["Resultado"] = "Existe"; return RedirectToAction("Create"); }
                    return View("Create", usuario);

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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
