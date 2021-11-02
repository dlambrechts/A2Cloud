using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ActivoEstadoAsignadoBE : ActivoEstadoBE
    {
        public override bool Asignar()
        {
            return false;
        }

        public override bool DarDeBaja()
        {
            return true;
        }

        public override bool Desasignar()
        {
            return true;
        }
    }
}
