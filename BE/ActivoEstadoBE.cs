using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class ActivoEstadoBE
    {
        private string codigo;

        private string descripcion;

        public string Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public abstract bool Asignar();
        public abstract bool Desasignar();
        public abstract bool DarDeBaja();
    }
}
