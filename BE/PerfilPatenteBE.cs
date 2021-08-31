using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PerfilPatenteBE: PerfilComponenteBE

    {
        public override IList<PerfilComponenteBE> Hijos
        {
            get { return new List<PerfilComponenteBE>(); }
        }
        public override void AgregarHijo(PerfilComponenteBE Comp) { }

        public override void QuitarHijo(PerfilComponenteBE Comp) { }

        public override void VaciarHijos() { }
    }
}
