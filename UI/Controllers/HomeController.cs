using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BE;
using GestorDeArchivo;

namespace UI.Controllers
{
   
    public class HomeController : Controller
    {

       
        UsuarioBLL bllUusuario = new UsuarioBLL();
        ActivoBLL bllActivo = new ActivoBLL();
        LicenciaBLL bllLicencia = new LicenciaBLL();
        ColaboradorBLL bllColaborador = new ColaboradorBLL();
        DepartamentoBLL bllDepartamento = new DepartamentoBLL();
        InteligenciaBLL bllInteligencia = new InteligenciaBLL();

        public ActionResult Index()
        {
            if (Session["IdUsuario"] != null) {

                ConfigurarIdioma();

                List<ActivoBE> Activos = bllActivo.Listar();
                List<LicenciaBE> Licencias = bllLicencia.Listar();

                // Cantidades Generales

                ViewBag.CantidadDispositivos = Activos.Count();
                ViewBag.CantidadContratosLicencia = Licencias.Count();
                ViewBag.CantidadDepartamentos = bllDepartamento.Listar().Count();
                ViewBag.CantidadColaboradores = bllColaborador.Listar().Count();

                int ActivosAsignados = Activos.Where(x => x.Estado.Codigo == "BE.ActivoEstadoAsignadoBE").ToList().Count();
                int ActivosDisponibles = Activos.Where(x => x.Estado.Codigo == "BE.ActivoEstadoDisponibleBE").ToList().Count();
                int ActivosBaja = Activos.Where(x => x.Estado.Codigo == "BE.ActivoEstadoBajaBE").ToList().Count();

                // Panel de Porcentajes

                if (Activos.Count > 0)
                {
                    ViewBag.PorcentajeActivosAsignados = (ActivosAsignados * 100) / Activos.Count();

                    ViewBag.PorcentajeActivosDisponibles = (ActivosDisponibles * 100) / Activos.Count();

                    ViewBag.PorcentajeActivosBaja = (ActivosBaja * 100) / Activos.Count();

                } else ViewBag.PorcentajeActivosAsignados = 0;

                if (Licencias.Count > 0)

                {
                    int LicenciasTotal = 0;
                    int LicenciasDisponible = 0;
                    foreach (LicenciaBE contrato in Licencias)
                    {
                        LicenciasTotal = LicenciasTotal + contrato.Cantidad;
                        LicenciasDisponible = LicenciasDisponible + contrato.Disponible;
                    }

                    int LicenciasAsignada = LicenciasTotal - LicenciasDisponible;

                    ViewBag.PorcentajeLicenciasAsignadas = (LicenciasAsignada * 100) / LicenciasTotal;
                }

                else { ViewBag.PorcentajeLicenciasAsignadas = 0; }


                // Notificaciones de Inteligencia

                if (bllInteligencia.AnalisisActivosOciosos().Count > 0) { ViewBag.AnalisisOciosos = "SI"; }

                if (bllInteligencia.AnalisisColaboradorActivos().Count > 0) { ViewBag.AnalisisColaborador = "SI"; }

                return View();
            }

            else { return RedirectToAction("Index", "Login"); }
        }



        public void ConfigurarIdioma()  // Se carga el Idioma del Usuario
        {
            try
            {
                IdiomaBLL bllIdioma = new IdiomaBLL();
                Session["Idiomas"] = bllIdioma.ObtenerIdiomas().Where(I => I.PorcentajeTraducido == 100);

                UsuarioBE user = new UsuarioBE();
                user.Id = Convert.ToInt32(Session["IdUsuario"]);
                user = bllUusuario.ObtenerUno(user);

                Session["IdiomaSelected"] = user.Idioma;
                Session["Traducciones"] = bllIdioma.ObtenerTraduccionesDic(user.Idioma);
            }

            catch (Exception ex) 
            
            {
                FileMananager.RegistrarError(ex.Message);
            }

        }


        
        public JsonResult TipoDispositivos() 
        
        {
            List<ActivoTipoBE> Tipos = new List<ActivoTipoBE>();
            Tipos = bllActivo.ListarTipos();

            Chart _chart = new Chart();

            _chart.labels = Tipos.Select(x => x.Descripcion).ToArray();
            _chart.datasets = new List<Datasets>();
            _chart.datasets.Add(new Datasets()
            {
 
                data = Tipos.Select(x => x.Cantidad).ToArray(),
                backgroundColor= new string[] { "#4e73df", "#1cc88a", "#9650a5", "#109b0d", "#edcd2c", "#b54040", "#0000FF" },
                
                hoverBackgroundColor = new string[] { "#2e59d9", "#17a673", "#bc14e1", "#19eb82", "#faf86b", "#ef0606", "#0000FF" },
            });

            return Json(_chart, JsonRequestBehavior.AllowGet);

        }




        public class Chart 
        
        { 
            public string [] labels { get; set; }

            public List<Datasets> datasets { get; set; }
        }

        public class Datasets { 
        


            public string[] backgroundColor { get; set; }
            public string[] hoverBackgroundColor { get; set; }
            public string[] hoverBorderColor { get; set; }

            public int[] data { get; set; }
        }

    }
}