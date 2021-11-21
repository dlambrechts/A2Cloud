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

            List<ActivoBE> Activos = new List<ActivoBE>();
            List<ColaboradorBE> Colaboradores = new List<ColaboradorBE>();

            Activos = ActivosSinAsignar();

            foreach (ActivoBE item in Activos)
            {
                if (item.Tipo.ArquitecturaPc == true)
                {
                    RecomendacionBE recomendacion = new RecomendacionBE();




                }
            }


            return Recomendaciones;


        }

        public List<ActivoBE> ActivosSinAsignar()  // Lista los activos con ciclo de vida vigente que no están asignados

        {

            List<ActivoBE> Activos = new List<ActivoBE>();

            Activos = mppActivo.Listar();

            Activos.Where(x => x.Estado.Asignar() == true).ToList();

            return Activos;

        }

        public bool CumpleConPerfil(ActivoBE Activo, ColaboradorBE Colaborador) // Verifica si un Dispositivo cumple con el perfil de un Colaborador
        {

            if (Activo.Tipo == Colaborador.PerfilHardware.DispositivoPrincipal
                && Activo.MemoriaRam >= Colaborador.PerfilHardware.MemoriaRamMinima
                && Activo.TamañoDisco >= Colaborador.PerfilHardware.AlmecenamientoMinimo
                && Activo.NucleosProcesador >= Colaborador.PerfilHardware.NucleosProcesadorMinimo
                && Activo.MemoriaVideo >= Colaborador.PerfilHardware.MemoriaVideoMinima
                && Activo.FrecuenciaProcesador >= Colaborador.PerfilHardware.FrecuenciaProcesadorMinima
                && Activo.AceleradoraGrafica == Colaborador.PerfilHardware.RequiereAceleradoraGrafica
                ) return true;

            else return false;

        }

        public List<ColaboradorBE>ColaboradoresSinDispositivoPrincipal()
        {
            List<ColaboradorBE> Colaboradores = new List<ColaboradorBE>();

            Colaboradores = mppColaborador.Listar();

            List<ColaboradorBE> SinDispositivoPrincipal = new List<ColaboradorBE>();

            List<AsignacionActivoBE> Asignaciones = new List<AsignacionActivoBE>();

            Asignaciones = mppAsignacion.Listar();
            Asignaciones = Asignaciones.Where(x => x.Estado.Id == 1 && x.Activo.Tipo.ArquitecturaPc == true).ToList();

            foreach(ColaboradorBE item in Colaboradores)

            {
                

            }

        }
    }
}
