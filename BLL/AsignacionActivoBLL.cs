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

        public void Insertar(AsignacionActivoBE Asignacion)

        {
            Asignacion.FechaCreacion = DateTime.Now;

            ActivoBLL bllActivo = new ActivoBLL();

            Asignacion.Activo.CambiarEstado(new ActivoEstadoAsignadoBE());

            Asignacion.Activo.FechaModificacion = DateTime.Now;
            Asignacion.Activo.UsuarioModificacion = Asignacion.UsuarioCreacion;

            Asignacion.Estado.Id = 1;

            if(Asignacion.Tipo.Id==1)
            {
                Asignacion.Ubicacion = Asignacion.Colaborador.Ubicacion;

            }

            else { Asignacion.Ubicacion = Asignacion.Colaborador.Localizacion.Ubicacion; }
            
            mppAsignacionActivo.Insertar(Asignacion);

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

    }
}
