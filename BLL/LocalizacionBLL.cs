using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class LocalizacionBLL
    {
        LocalizacionMPP mppLocalizacion = new LocalizacionMPP();
        public void Insertar(LocalizacionBE Localizacion)

        {
            Localizacion.FechaCreacion = DateTime.Now;

            mppLocalizacion.Insertar(Localizacion);

        }

        public List<LocalizacionBE> Listar()

        {
            return mppLocalizacion.Listar();
        }


        public void Editar(LocalizacionBE Localizacion)

        {
            Localizacion.FechaModificacion = DateTime.Now;

            mppLocalizacion.Editar(Localizacion);
        }

        public LocalizacionBE ObtenerUno(LocalizacionBE Localizacion)

        {
            return mppLocalizacion.ObtenerUno(Localizacion);
        }

        public void Eliminar(LocalizacionBE Localizacion)

        {
            Localizacion.FechaModificacion = DateTime.Now;

            mppLocalizacion.Eliminar(Localizacion);

        }
    }
}
