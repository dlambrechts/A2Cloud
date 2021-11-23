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

            Activos = mppActivo.Listar().Where(x => x.Tipo.ArquitecturaPc == true).ToList();

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

                    if (DispositivosCandidatos.Count > 0) 
                    { 
                        string stringDispCandidatos = string.Join(", ", DispositivosCandidatos);
                        RecomendacionBE recomendacion = new RecomendacionBE();

                        recomendacion.Hallazgo = "El colaborador " + colaborador.NombreCompleto + " no tiene Dispositivo principal asignado";
                        recomendacion.Propuesta = "Los siguientes activos estan disponibles y cumplen con el perfil del colaborador: " + stringDispCandidatos;

                        Recomendaciones.Add(recomendacion);
                    }
                }

                else 
                
                {
                    DispositivosCandidatos.Clear();

                    foreach (ActivoBE activo in Activos)
                    {
                        if (CumpleConPerfil(activo, colaborador) == true)
                        {
                            DispositivosCandidatos.Add(activo);

                        }

                    }

                    if (DispositivosCandidatos.Count > 0)
                    {
                        string stringDispCandidatos = string.Join(", ", DispositivosCandidatos);
                        RecomendacionBE recomendacion = new RecomendacionBE();

                        recomendacion.Hallazgo = "El Dispositivo Principal de " + colaborador.NombreCompleto + " no comple con los requerimientos de su Perfil de Hardware";
                        recomendacion.Propuesta = "Los siguientes activos estan disponibles y cumplen con el perfil del colaborador: " + stringDispCandidatos;

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
    }
}
