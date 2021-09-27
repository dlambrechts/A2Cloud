using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BLL;
using Seguridad;
using X.PagedList;

namespace UI.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioBLL bllU = new UsuarioBLL();
        IdiomaBLL bllIdioma = new IdiomaBLL();

        // GET: Usuario
        public ActionResult Index(int? pagina, string Dato_Buscar, string Valor_Filtro)
        {
            if (Session["IdUsuario"] != null)
            {
                UsuarioBLL bllUsuario = new UsuarioBLL();
                List<UsuarioBE> Usuarios = bllUsuario.ListarTodos();
                ViewBag.Resultado = TempData["Resultado"] as string;
                ViewBag.Usuario = TempData["IdUsuario"] as string;
                ViewBag.Mail = TempData["Mail"] as string;
                if (Dato_Buscar != null)
                {
                    pagina = 1;
                }
                else
                {
                    Dato_Buscar = Valor_Filtro;
                }

                ViewBag.ValorFiltro = Dato_Buscar;

                if (!String.IsNullOrEmpty(Dato_Buscar))
                {
                    Usuarios= Usuarios.Where(u => u.Nombre.ToUpper().Contains(Dato_Buscar.ToUpper())
                        || u.Apellido.ToUpper().Contains(Dato_Buscar.ToUpper())
                        || u.Credencial.Mail.ToUpper().Contains(Dato_Buscar.ToUpper())
                        ).ToList();
                }

                int RegistrosPorPagina = 10;
                int Indice = pagina.HasValue ? Convert.ToInt32(pagina) : 1;

                return View(Usuarios.ToPagedList(Indice, RegistrosPorPagina));
            }

            else { return RedirectToAction("Index", "Login"); }

        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            if (Session["IdUsuario"] == null) return RedirectToAction("Index", "Login");
            UsuarioBE user = new UsuarioBE();
            user.Id = id;
            user=bllU.ObtenerUno(user);
            return View(user);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] != null)
            {
                ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas().Where(Idioma=>Idioma.PorcentajeTraducido==100);
                ViewBag.Resultado = TempData["Resultado"] as string;
                return View();
            }
            else { return RedirectToAction("Index", "Login"); }
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(UsuarioBE usuario)
        {
            try
            {
                ModelState.Remove("Idioma.Descripcion");
                if (ModelState.IsValid && bllU.ValidarExistencia(usuario.Credencial)==false)
                    {
                        usuario.Credencial.Contraseña = Encriptado.Hash(usuario.Credencial.Contraseña);

                        usuario.UsuarioCreacion = new UsuarioBE();
                        usuario.UsuarioCreacion.Id = Convert.ToInt32(Session["IdUsuario"]);
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
            if (Session["IdUsuario"] == null) return RedirectToAction("Index", "Login");
            UsuarioBE us = new UsuarioBE();
            us.Id = id;
            us=bllU.ObtenerUno(us);


            ViewBag.Resultado = TempData["Editado"] as string;
            ViewData["Idiomas"] = bllIdioma.ObtenerIdiomas().Where(Idioma => Idioma.PorcentajeTraducido == 100);
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
                ModelState.Remove("Idioma.Descripcion");


                if (ModelState.IsValid)  // Falta validar que si cambia el mail, no exista
                    {
                        usuario.UsuarioModificacion = new UsuarioBE();
                        usuario.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);
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

        // GET: CambiarContraseña
        public ActionResult CambiarContraseña(int id)
        {
            if (Session["IdUsuario"] == null) return RedirectToAction("Index", "Login");

            UsuarioBE user = new UsuarioBE();
            user.Id = id;
            bllU.ObtenerUno(user);

            ViewBag.Resultado = TempData["Resultado"] as string;

            return View();
                

        }

        // POST: Usuario/CambiarContraseña
        [HttpPost]
        public ActionResult CambiarContraseña(UsuarioBE usuario, FormCollection collection)
        {
            try
            {
                ModelState.Remove("Nombre");
                ModelState.Remove("Credencial.Mail");
                if (ModelState.IsValid)  
                {

                    string ContraseñaActual = Request.Form["ContraseñaActual"];
                    string ContraseñaNueva = usuario.Credencial.Contraseña;

                    usuario = bllU.ObtenerUno(usuario);

                    if (usuario.Credencial.Contraseña.Equals(Encriptado.Hash(ContraseñaActual)))
                        
                    {
                        usuario.Credencial.Contraseña = Encriptado.Hash(ContraseñaNueva);
                        usuario.UsuarioModificacion = new UsuarioBE();
                        usuario.UsuarioModificacion.Id = Convert.ToInt32(Session["IdUsuario"]);

                        bllU.CambiarContraseña(usuario);
                        TempData["Resultado"] = "Ok";

                        return RedirectToAction("CambiarContraseña");

                    }

                    else 
                    
                    {
                        TempData["Resultado"] = "ContraseñaIncorrecta";
                        return RedirectToAction("CambiarContraseña");
                        
                    }

                    
                }

                else
                {
                    return View(usuario);
                }
             }
            catch
            {
                return View();
            }
        }
    }
}
