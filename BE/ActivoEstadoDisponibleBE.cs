using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ActivoEstadoDisponibleBE:ActivoEstadoBE
    {
        public override bool Asignar()
        {
            return true;
        }

        public override bool DarDeBaja()
        {
            return true;
        }

        public override bool Desasignar()
        {
            return false;
        }

        public override bool Eliminar()
        {
            return true;
        }
    }
}
