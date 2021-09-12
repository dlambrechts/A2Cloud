using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BLL;

namespace UI.Controllers
{
    public class DbController : Controller
    {
        BitacoraBLL bllBit = new BitacoraBLL();
        DigitoVerificadorBLL bllDv = new DigitoVerificadorBLL();


        // GET: Integridad
        public ActionResult Integridad()
        {
            if (Session["IdUsuario"] != null)
            {
                return View();
            }
            else { return RedirectToAction("Index", "Login"); }
        }


        public JsonResult EjecutarAnalisis()
        {
            try
            {
                

                CredencialBE cred = new CredencialBE();
                UsuarioBE user = new UsuarioBE(cred);

                user.Id = Convert.ToInt32(Session["IdUsuario"]);
                user.Credencial.Mail = Session["Mail"].ToString();


                if (bllDv.VerificarIntegridad(user)) 
                
                {
                    return Json(new { success = true });
                }

                else return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
            catch 
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

















        // GET: Db
        public ActionResult Index()
        {
            return View();
        }

        // GET: Db/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Db/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Db/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Db/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Db/Edit/5
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

        // GET: Db/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Db/Delete/5
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
