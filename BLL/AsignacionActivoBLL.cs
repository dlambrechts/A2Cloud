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
