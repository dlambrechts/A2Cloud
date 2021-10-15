using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class ActivoBLL
    {
        ActivoMPP mppActivo = new ActivoMPP();

        public List<ActivoTipoBE> ListarTipos() 
        
        {
            return mppActivo.ListarTipos();        
        }

        public List<ActivoBE> Listar() 
        
        {
            return mppActivo.Listar();
        }
    }
}
