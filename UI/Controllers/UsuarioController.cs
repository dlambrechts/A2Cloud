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
        IdiomaBLL bllIdioma = new IdiomaBLL();

        // GET: Usuario
        public ActionResult Index()
        {
            UsuarioBLL bllUsuario = new UsuarioBLL();
            var lista=bllUsuario.ListarTodos();
            ViewBag.Resultado = TempData["Resultado"] as string;
            ViewBag.Usuario = TempData["IdUsuario"] as string;
            ViewBag.Mail = TempData["Mail"] as string;
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

            UsuarioBE us = new UsuarioBE();
            us.Id = id;
            us=bllU.ObtenerUno(us);

            ViewBag.Resultado = TempData["Editado"] as string;
            ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas();
            return View(us);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UsuarioBE usuario)
        {
            try
            {
                ModelState.Remove("Credencial.Contraseña");
                ModelState.Remove("Credencial.ConfirmarCont");

                if (ModelState.IsValid)  // Falta validar que si cambia el mail no exista, excluir el usuario que se esta editando de esa validación
                    {
                        bllU.Editar(usuario);
                        TempData["Resultado"] = "Editado";
                        TempData["IdUsuario"] = usuario.Id.ToString();
                    return RedirectToAction("Index");
                    }


                    else
                    {
                        IdiomaBLL bllIdioma = new IdiomaBLL();
                        ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas();
                        return View("Edit", usuario);

                    }
                }
                catch
                {
                    return View();
                }

        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            UsuarioBE usu = new UsuarioBE();
            usu.Id = id;
            usu=bllU.ObtenerUno(usu);

            return View(usu);
         
            
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            UsuarioBE delU = new UsuarioBE();
            delU.Id = id;
            delU = bllU.ObtenerUno(delU);
            try
            {
                // TODO: Add delete logic here
                bllU.Eliminar(delU);
                TempData["Resultado"] = "Eliminado";
                TempData["IdUsuario"] = delU.Id.ToString();
                TempData["Mail"] = delU.Credencial.Mail;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
