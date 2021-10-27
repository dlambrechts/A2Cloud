using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ActivoTipoBE:EntityBE
    {
        private string descripcion;
        public string Descripcion

        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
        private bool arquitecturaPc;
        public bool ArquitecturaPc { get => arquitecturaPc; set => arquitecturaPc = value; }       

        private int cantidad;
        public int Cantidad { get => cantidad; set => cantidad = value; }

    }
}
