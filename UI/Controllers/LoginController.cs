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
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
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




                                // Reiniciar Contador
                                // Iniciar Sesión
                                Session["IdUsuario"] = user.Id.ToString();
                                Session["NombreCompleto"] = user.Nombre.ToString() + " " + user.Apellido.ToString();
                         
                                return RedirectToAction("Index", "Home");
                            }

                            else
                            {
                                // Contraseña incorrecta
                                // Intentos Fallidos +1

                            }
                                               
                        }
                        else
                        {
                            // Usuario Bloqueado

                        }
                    
                    }
                    
                    else 
                    
                    { 
                        // Mensaje "verifique los datos"
                    }
                }
                return View("Index");

            }
            catch
            {
                return View();
            }
        }



    }
}
