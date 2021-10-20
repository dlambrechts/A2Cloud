using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class PerfilDeHardwareBLL
    {
        PerfilDeHardwareMPP mppPerfilDeHardware = new PerfilDeHardwareMPP();
        public List<PerfilDeHardwareBE> Listar()

        {
            return mppPerfilDeHardware.Listar();
        }

        public PerfilDeHardwareBE ObtenerUno(PerfilDeHardwareBE PerfilDeHardware)

        {
            return mppPerfilDeHardware.ObtenerUno(PerfilDeHardware);
        }
    }
}
