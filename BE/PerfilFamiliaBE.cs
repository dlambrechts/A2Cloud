using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PerfilFamiliaBE:PerfilComponenteBE
    {
        private IList<PerfilComponenteBE> _hijos;
        public PerfilFamiliaBE()
        {
            _hijos = new List<PerfilComponenteBE>();
        }

        public override IList<PerfilComponenteBE> Hijos
        {
            get
            {
                return _hijos.ToArray();
            }
        }

        public override void VaciarHijos()
        {
            _hijos = new List<PerfilComponenteBE>();
        }
        public override void AgregarHijo(PerfilComponenteBE Comp)
        {
            _hijos.Add(Comp);
        }
        public override void QuitarHijo(PerfilComponenteBE Comp)

        {
            _hijos.Remove(Hijos.Where(_hijos => _hijos.Id == Comp.Id).First());
        }
    }

}
