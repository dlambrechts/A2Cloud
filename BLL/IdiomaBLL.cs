using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
   public  class IdiomaBLL
    {
        IdiomaMPP mppIdioma = new IdiomaMPP();
        
        public List<IdiomaBE> ObtenerIdiomas()

        {           
            return mppIdioma.ObtenerIdiomas();
        }
    }
}
