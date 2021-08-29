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
        public ActionResult Login(CredencialBE cred)
        {
            try
            {



                if (ModelState.IsValid)

                {
                    if (bllUser.ValidarExistencia(cred))

                    {
                        UsuarioBE user = new UsuarioBE();
                        user = bllUser.ObtenerUno(user);

                        if (user.Activo)

                        {
                            if (user.Credencial.Contraseña.Equals(Encriptado.Hash(cred.Contraseña)))
                            
                            
                            {

                                // Reiniciar Contador
                                // Iniciar Sesión
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



    }
}
