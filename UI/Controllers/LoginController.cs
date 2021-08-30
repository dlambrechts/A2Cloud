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
    public class LoginController : Controller
    {
        UsuarioBLL bllUser = new UsuarioBLL();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Resultado = TempData["Resultado"] as string;

            if (Session["IdUsuario"] == null)
             {
                return View();
              }
            else { return RedirectToAction("Index", "Home"); }
        }



        // POST: Login/Index
        [HttpPost]
        public ActionResult Index(CredencialBE cred)
        {
  
                try
                {

                    ModelState.Remove("Contraseña");
                    ModelState.Remove("ConfirmarCont");

                    if (ModelState.IsValid)

                    {
                        if (bllUser.ValidarExistencia(cred))

                        {
                            UsuarioBE user = new UsuarioBE();

                            user = bllUser.ObtenerPorMail(cred);

                            if (user.Activo)

                            {
                                string ingresada = cred.Contraseña;
                                string ingresadaEnc = Encriptado.Hash(cred.Contraseña);

                                string contraseñaReal = user.Credencial.Contraseña;

                                if (user.Credencial.Contraseña.Equals(Encriptado.Hash(cred.Contraseña)))

                                {
                                    bllUser.ReiniciarContador(user); // Reiniciar Contador

                                    // Iniciar Sesión
                                    Session["IdUsuario"] = user.Id.ToString();
                                    Session["NombreCompleto"] = user.Nombre.ToString() + " " + user.Apellido.ToString();

                                    return RedirectToAction("Index", "Home");
                                }

                                else
                                {
                                    // Contraseña incorrecta
                                    // Intentos Fallidos +1
                                    bllUser.IncrementarIntentosFallidos(user);
                                    if (user.IntentosFallidos == 3)

                                    {
                                        bllUser.BloquarUsuario(user);
                                    }

                                     TempData["Resultado"] = "Contraseña Incorrecta";
                                     return RedirectToAction("Index");
                            }

                            }
                            else
                            {
                            // El usuario está bloqueado
                            TempData["Resultado"] = "Usuario Bloqueado, comuniquese con el Administrador";
                            return RedirectToAction("Index");

                        }

                        }

                        else

                        {
                        // El usuario no existe
                        TempData["Resultado"] = "Usuario Inexistente";
                        return RedirectToAction("Index");
                    }
                    }
                    return View("Index");

                }
                catch
                {
                    return View();
                }

         }

        // GET: Logout
        public ActionResult Logout()
        {


            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }






    }
}
