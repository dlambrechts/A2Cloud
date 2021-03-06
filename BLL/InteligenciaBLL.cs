using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;


namespace BLL
{
    public class InteligenciaBLL
    {
        ActivoMPP mppActivo = new ActivoMPP();
        ColaboradorMPP mppColaborador = new ColaboradorMPP();
        AsignacionActivoMPP mppAsignacion = new AsignacionActivoMPP();


        public List<RecomendacionBE> AnalisisActivosOciosos()

        {
            List<RecomendacionBE> Recomendaciones = new List<RecomendacionBE>();

            List<ActivoBE> ActivosDisponibles = new List<ActivoBE>();
            List<ColaboradorBE> ColaboradoresSin = new List<ColaboradorBE>();

            ActivosDisponibles = ActivosSinAsignar();
            ColaboradoresSin = ColaboradoresSinDispositivoPrincipal();

            List<ColaboradorBE> candidatos = new List<ColaboradorBE>();

            foreach (ActivoBE activo in ActivosDisponibles)
            {

                candidatos.Clear();

                foreach (ColaboradorBE colaborador in ColaboradoresSin)
                {


                    if (CumpleConPerfil(activo, colaborador))
                    {
                        candidatos.Add(colaborador);
                    }

                }

                if (candidatos.Count > 0)
                { 
                RecomendacionBE recomendacion = new RecomendacionBE();

                recomendacion.Hallazgo = "El dispositivo " + activo.DescripcionLarga + " no está siendo utilizado actualmente.";

                    string stringCandidatos = string.Join(", ", candidatos);
                    recomendacion.Propuesta = "El dispositivo se ajusta al perfil de " + stringCandidatos +" y aún no tienen un dispositivo principal asignado." ;

                Recomendaciones.Add(recomendacion);
                }
            }


            return Recomendaciones;

        }

        public List<RecomendacionBE> AnalisisColaboradorActivos() 
        
        {
            List<RecomendacionBE> Recomendaciones = new List<RecomendacionBE>();
            List<ColaboradorBE> Colaboradores = new List<ColaboradorBE>();
            List<ColaboradorBE> ColaboradoresSin = new List<ColaboradorBE>();
            List<ActivoBE> Activos = new List<ActivoBE>();

            Activos = mppActivo.Listar().Where(x => x.Tipo.ArquitecturaPc == true && x.Estado.Asignar()==true).ToList(); // Dispositivos de tipo Principal que no están asignados

            List<ActivoBE> DispositivosCandidatos = new List<ActivoBE>();


            Colaboradores = mppColaborador.Listar();
            ColaboradoresSin = ColaboradoresSinDispositivoPrincipal();

            foreach (ColaboradorBE colaborador in Colaboradores)

            {
                if (ColaboradoresSin.Exists(x => x.Id == colaborador.Id))
                {

                    DispositivosCandidatos.Clear();

                    foreach (ActivoBE activo in Activos)
                    {
                        if (CumpleConPerfil(activo, colaborador) == true)
                        {
                            DispositivosCandidatos.Add(activo);

                        }

                    }

                    RecomendacionBE recomendacion = new RecomendacionBE();
                    recomendacion.Hallazgo = "El colaborador " + colaborador.NombreCompleto + " no tiene Dispositivo Principal asignado";

                    if (DispositivosCandidatos.Count > 0) 
                    { 
                        string stringDispCandidatos = string.Join(", ", DispositivosCandidatos);
                        
                        recomendacion.Propuesta = "Los siguientes activos estan disponibles y cumplen con el perfil del colaborador: " + stringDispCandidatos;

                        
                    }

                    else 
                    
                    {
                        recomendacion.Propuesta = "No hay Dispositivos disponibles que cumplan con el perfil de " + colaborador.PerfilHardware.Descripcion + ", deberá incorporar un nuevo Dispositivo";
                    }

                    Recomendaciones.Add(recomendacion);
                }

                else 
                
                {
                    DispositivosCandidatos.Clear();

                    List<ActivoBE> DispColaborador = new List<ActivoBE>();
                    DispColaborador = AsignacionesActivasColaborador(colaborador);

                    bool ContemplarColaborador = true;

                    foreach (ActivoBE activo in DispColaborador)
                    {
                        if (CumpleConPerfil(activo, colaborador) == false)
                        {
                            foreach(ActivoBE act in Activos)

                                if (CumpleConPerfil(act, colaborador)) 
                                { 
                                    DispositivosCandidatos.Add(activo);
                                }

                        }

                        else { ContemplarColaborador = false; }

                    }

                    if (ContemplarColaborador) 
                    { 
                        RecomendacionBE recomendacion = new RecomendacionBE();

                        recomendacion.Hallazgo = "El Dispositivo Principal de " + colaborador.NombreCompleto + " no cumple con los requerimientos de su Perfil de Hardware";

                        if (DispositivosCandidatos.Count > 0)
                        {
                            string stringDispCandidatos = string.Join(", ", DispositivosCandidatos);

                            recomendacion.Propuesta = "Los siguientes activos estan disponibles y cumplen con el perfil del colaborador: " + stringDispCandidatos;

                        
                        }

                        else 
                    
                        {
                            recomendacion.Propuesta = "No hay Dispositivos disponibles que cumplan con el perfil de " + colaborador.PerfilHardware.Descripcion +", deberá incorporar un nuevo Dispositivo" ;
                        }

                        Recomendaciones.Add(recomendacion);
                    }
                }

            }
         

            return Recomendaciones;
        
        }

   
        private List<ActivoBE> ActivosSinAsignar()  // Lista los activos con ciclo de vida vigente que no están asignados

        {

            List<ActivoBE> Activos = new List<ActivoBE>();

            Activos = mppActivo.Listar();

            Activos=Activos.Where(x => x.Estado.Asignar() == true).ToList();

            return Activos;

        }

        private bool CumpleConPerfil(ActivoBE Activo, ColaboradorBE Colaborador) // Verifica si un Dispositivo cumple con el perfil de un Colaborador
        {

            if (Activo.Tipo.Id == Colaborador.PerfilHardware.DispositivoPrincipal.Id
                && Activo.MemoriaRam >= Colaborador.PerfilHardware.MemoriaRamMinima
                && Activo.TamañoDisco >= Colaborador.PerfilHardware.AlmecenamientoMinimo
                && Activo.NucleosProcesador >= Colaborador.PerfilHardware.NucleosProcesadorMinimo
                && Activo.MemoriaVideo >= Colaborador.PerfilHardware.MemoriaVideoMinima
                && Activo.FrecuenciaProcesador >= Colaborador.PerfilHardware.FrecuenciaProcesadorMinima
                && Activo.AceleradoraGrafica == Colaborador.PerfilHardware.RequiereAceleradoraGrafica
                ) return true;

            else return false;

        }

        private List<ColaboradorBE>ColaboradoresSinDispositivoPrincipal() //
        {
            List<ColaboradorBE> Colaboradores = new List<ColaboradorBE>();

            Colaboradores = mppColaborador.Listar();

            List<ColaboradorBE> SinDispositivoPrincipal = new List<ColaboradorBE>();

            List<AsignacionActivoBE> Asignaciones = new List<AsignacionActivoBE>();

            Asignaciones = mppAsignacion.Listar();
            Asignaciones = Asignaciones.Where(x => x.Estado.Id == 1 && x.Activo.Tipo.ArquitecturaPc == true).ToList();

            foreach(ColaboradorBE item in Colaboradores)

            {
                if (Asignaciones.Exists(x => x.Colaborador.Id == item.Id && x.Activo.Tipo.Id == item.PerfilHardware.DispositivoPrincipal.Id)){
                     }
                else { SinDispositivoPrincipal.Add(item); }

            }

            return SinDispositivoPrincipal;

        }

        private List<ActivoBE> AsignacionesActivasColaborador(ColaboradorBE Colaborador)

        {
            List<ActivoBE> Dispositivos = new List<ActivoBE>();
            List<AsignacionActivoBE> Asignaciones = mppAsignacion.Listar();

            Asignaciones = Asignaciones.Where(x => x.Estado.Id == 1 && x.Colaborador.Id == Colaborador.Id ).ToList();

            if (Asignaciones.Count == 0) { return null; }

            else 
            
            {
                foreach(AsignacionActivoBE item in Asignaciones)
                {

                    Dispositivos.Add(item.Activo);
                }    
                return Dispositivos;
            }
            
        }
    }
}
