using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        UsuarioBLL bllUusuario = new UsuarioBLL();
        public ActionResult Index()
        {
            if (Session["IdUsuario"] != null) {

                ConfigurarIdioma();
                return View();
            }

            else { return RedirectToAction("Index", "Login"); }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void ConfigurarIdioma()
        {
            IdiomaBLL bllIdioma = new IdiomaBLL();
            Session["Idiomas"] = bllIdioma.ObtenerIdiomas();

            UsuarioBE user = new UsuarioBE();
            user.Id = Convert.ToInt32(Session["IdUsuario"]);
            user = bllUusuario.ObtenerUno(user);

            Session["IdiomaSelected"] = user.Idioma;
            Session["Traducciones"] = bllIdioma.ObtenerTraduccionesDic(user.Idioma);


            //if (Session["IdUsuario"] == null || Session["IdiomaSelected"] == null)
            //{
            //    IdiomaBE IdiomaDefecto = bllIdioma.ObtenerIdiomaPorDefecto();
            //    Session["IdiomaSelected"] = IdiomaDefecto;
            //    Session["Traducciones"] = bllIdioma.ObtenerTraduccionesDic(IdiomaDefecto);
            //}


        }
    }
}