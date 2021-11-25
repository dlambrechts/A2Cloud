using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ActivoEstadoBajaBE: ActivoEstadoBE
    {
        public override bool Asignar()
        {
            return false;
        }
        public override bool DarDeBaja()
        {
            return false;
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
