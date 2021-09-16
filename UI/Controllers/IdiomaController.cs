﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;

namespace UI.Controllers
{
    public class IdiomaController : Controller
    {
        IdiomaBLL bllId = new IdiomaBLL();
        // GET: Idioma
        public ActionResult Index()
        {

            if (Session["IdUsuario"] != null)
            {

                return View(bllId.ObtenerIdiomas());
            }

            else { return RedirectToAction("Index", "Login"); }
        }

        // GET: Idioma/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Idioma/Create
        public ActionResult Create()
        {
            if (Session["IdUsuario"] != null)
            {
                return View();

            }

            else { return RedirectToAction("Index", "Login"); }
        }

        // POST: Idioma/Create
        [HttpPost]
        public ActionResult Create(IdiomaBE idioma)
        {

            if (Session["IdUsuario"] != null)
            {

                try
                {
                    UsuarioBE user = new UsuarioBE();

                    user.Id = Convert.ToInt32(Session["IdUsuario"]);

                    idioma.UsuarioCreacion = user;

                    bllId.Insertar(idioma);



                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
       

            }

            else { return RedirectToAction("Index", "Login"); }

        }

        // GET: Idioma/Edit/5
        public ActionResult Edit(int id)
        {


            if (Session["IdUsuario"] != null)
            {

                try
                {
                    IdiomaBE idioma = new IdiomaBE();

                    idioma.Id = id;

                    idioma = bllId.ObtenerUno(idioma);

                    return View(idioma);
                }
                catch 
                {
                    return View();
                }

            }
            else { return RedirectToAction("Index", "Login"); }
        }

        // POST: Idioma/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IdiomaBE idioma)
        {

            if (Session["IdUsuario"] != null)
            {
                try
                {

                    UsuarioBE user = new UsuarioBE();
                    user.Id = Convert.ToInt32(Session["IdUsuario"]);

                    idioma.UsuarioModificacion = user;
                    
                    bllId.Editar(idioma);



                    return RedirectToAction("Index");

                }
                catch 
                {
                    return View();
                }
            }

            else { return RedirectToAction("Index", "Login"); }

        }

        // GET: Idioma/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["IdUsuario"] != null)
            {
                IdiomaBE Idioma = new IdiomaBE();
                Idioma.Id = id;
                Idioma = bllId.ObtenerUno(Idioma);

                return View(Idioma);
            }

            else { return RedirectToAction("Index", "Login"); }
        }

        // POST: Idioma/Delete/5
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

        // GET: Idioma/Traduccion/5
        public ActionResult Traduccion(int id)
        {


            if (Session["IdUsuario"] != null)
            {

                try
                {
                    IdiomaBE idioma = new IdiomaBE();
                    idioma.Id = id;
                    List<IdiomaTraduccionBE> traducciones = new List<IdiomaTraduccionBE>();                  

                    traducciones = bllId.ObtenerTraducciones(idioma);
                   
                    return View(traducciones);
                }
                catch
                {
                    return View();
                }

            }
            else { return RedirectToAction("Index", "Login"); }
        }


        public JsonResult GuardarTraduccion(int Idioma,int Etiqueta,string Traduccion)
        {


                try
                {
                    IdiomaTraduccionBE traduccion = new IdiomaTraduccionBE();
                    traduccion.Idioma.Id = Idioma;
                    traduccion.Etiqueta.Id = Etiqueta;
                    traduccion.Texto = Traduccion;
                    bllId.GuardarTraduccion(traduccion);

                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);

                }
                catch
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }


        }

        public ActionResult CambiarIdioma(int id)
        {


            IdiomaBE Idioma = new IdiomaBE();
            Idioma.Id = id;
            Idioma=bllId.ObtenerUno(Idioma);

            Dictionary<string, IdiomaTraduccionBE> Traducciones = bllId.ObtenerTraduccionesDic(Idioma);

          
            if (Traducciones.Count() > 0)
                Session["Traducciones"] = Traducciones;
            else
            {
                Session["IdiomaSelected"] = Session["IdiomaSelected"]; //Si falla dejo seleccionado en el combo el que tenia previamente en session.(Si no hago esto queda seleccionado el último)
                return Json("Failed", JsonRequestBehavior.AllowGet);
            }
            Session["IdiomaSelected"] = Idioma;
            return Json("Success", JsonRequestBehavior.AllowGet);
        }



    }
}
