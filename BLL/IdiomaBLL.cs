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

        public void Insertar(IdiomaBE Idioma)
        
        
        {
            Idioma.FechaCreacion = DateTime.Now;

            mppIdioma.Insertar(Idioma);
        }

        public IdiomaBE ObtenerUno(IdiomaBE idioma) 
        
        {

            return mppIdioma.ObtenerUno(idioma);
        }

        public void Editar (IdiomaBE idioma) 
        
        {
            idioma.FechaModificacion = DateTime.Now;
            mppIdioma.Editar(idioma);
        }

    }
}
