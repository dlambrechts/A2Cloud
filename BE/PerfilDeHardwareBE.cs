using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PerfilDeHardwareBE:EntityBE
    {
        private string descripcion;

        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
