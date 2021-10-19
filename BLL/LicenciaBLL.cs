using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class LicenciaBLL
    {
        LicenciaMPP mppLic = new LicenciaMPP();

        public void Insertar(LicenciaBE Licencia)

        {
            Licencia.FechaCreacion = DateTime.Now;

            mppLic.Insertar(Licencia);

        }

        public void Editar(LicenciaBE Licencia)

        {
            Licencia.FechaModificacion = DateTime.Now;

            mppLic.Editar(Licencia);
        }

        public List<LicenciaBE> Listar()

        {
            return mppLic.Listar();
        }

        public LicenciaBE ObtenerUno(LicenciaBE Licencia)

        {
            return mppLic.ObtenerUno(Licencia);
        }

        public void Eliminar(LicenciaBE Licencia)

        {
            Licencia.FechaModificacion = DateTime.Now;

            mppLic.Eliminar(Licencia);

        }

        public List<LicenciaModalidadBE> ListarModalidades() 
        
        {
            return mppLic.ModalidadListar();
        }

    }
}
