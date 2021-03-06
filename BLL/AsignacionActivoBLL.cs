using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class AsignacionActivoBLL
    {
        AsignacionActivoMPP mppAsignacionActivo = new AsignacionActivoMPP();
        ActivoBLL bllActivo = new ActivoBLL();
        public void Insertar(AsignacionActivoBE Asignacion)

        {
            Asignacion.FechaCreacion = DateTime.Now;

            

            Asignacion.Activo.CambiarEstado(new ActivoEstadoAsignadoBE());

            Asignacion.Activo.FechaModificacion = DateTime.Now;
            Asignacion.Activo.UsuarioModificacion = Asignacion.UsuarioCreacion;

            Asignacion.Estado.Id = 1;

            if (Asignacion.Colaborador.FullRemoto == true) { Asignacion.Tipo.Id = 1; }

            if (Asignacion.Tipo.Id == 1)

            {
                Asignacion.Ubicacion = Asignacion.Colaborador.Ubicacion;

            }

            else { Asignacion.Ubicacion = Asignacion.Colaborador.Localizacion.Ubicacion; }

            mppAsignacionActivo.Insertar(Asignacion);

            bllActivo.CambiarEstado(Asignacion.Activo);

        }

        public void Finalizar(AsignacionActivoBE Asignacion) 
        
        {
            Asignacion.FechaModificacion= DateTime.Now;
            Asignacion.Activo.CambiarEstado(new ActivoEstadoDisponibleBE());
            Asignacion.Activo.UsuarioModificacion = Asignacion.UsuarioModificacion;

            mppAsignacionActivo.Finalizar(Asignacion);

            bllActivo.CambiarEstado(Asignacion.Activo);

        }
        public List<AsignacionActivoBE> Listar()

        {

            return mppAsignacionActivo.Listar();

        }


        public List<AsignacionTipoBE> ListarTipoAsignacion()

        {
            return mppAsignacionActivo.ListarTipoAsignacion();

        }

        public AsignacionTipoBE TipoAsignacionObtenerUno(AsignacionTipoBE AsignacionTipo)

        {

            return mppAsignacionActivo.TipoAsignacionObtenerUno(AsignacionTipo);
        }

        public List<AsignacionEstadoBE> ListarEstados() 
        
        {
            return mppAsignacionActivo.ListarEstados();
        }

        public AsignacionActivoBE ObtenerUno(AsignacionActivoBE Asignacion) 
        
        {

            return mppAsignacionActivo.ObtenerUno(Asignacion);
        }

    }
}
