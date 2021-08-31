using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class PerfilComponenteBE:EntityBE
    {
        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public abstract IList<PerfilComponenteBE> Hijos { get; }
        public abstract void AgregarHijo(PerfilComponenteBE Comp);
        public abstract void QuitarHijo(PerfilComponenteBE Comp);
        public abstract void VaciarHijos();
        public PerfilPermisoBE Permiso { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
