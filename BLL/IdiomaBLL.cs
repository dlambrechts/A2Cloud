using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class IdiomaBLL
    {
        IdiomaMPP mppIdioma = new IdiomaMPP();

        public List<IdiomaBE> ObtenerIdiomas()

        {
            return mppIdioma.ObtenerIdiomas();
        }

        public IdiomaBE ObtenerIdiomaPorDefecto()

        {
            IdiomaBE Idioma = new IdiomaBE();

            Idioma = mppIdioma.ObtenerIdiomas().First(x => x.PorDefecto == true);

            return Idioma;
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

        public void Editar(IdiomaBE idioma)

        {
            idioma.FechaModificacion = DateTime.Now;
            mppIdioma.Editar(idioma);
        }

        public void Eliminar(IdiomaBE Idioma)

        {
            Idioma.FechaModificacion = DateTime.Now;
            mppIdioma.Eliminar(Idioma);
        }

        public List<IdiomaEtiquetaBE> ObtenerEtiquetas()

        {
            return mppIdioma.ObtenerEtiquetas();
        }

        public Dictionary<string, IdiomaTraduccionBE> ObtenerTraduccionesDic(IdiomaBE Idioma)

        {
            return mppIdioma.ObtenerTraduccionesDic(Idioma);

        }

        public List<IdiomaTraduccionBE> ObtenerTraducciones(IdiomaBE Idioma)
        {

            return mppIdioma.ObtenerTraducciones(Idioma);
        }

        public void GuardarTraduccion(IdiomaTraduccionBE traduccion)
        {

            mppIdioma.GuardarTraduccion(traduccion);
        }

    }
}


