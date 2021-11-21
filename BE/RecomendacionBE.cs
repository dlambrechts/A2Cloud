using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RecomendacionBE
    {
        private int difucultad;
        private string hallazgo;
        private string propuesta;

        public int Difucultad { get => difucultad; set => difucultad = value; }
        public string Hallazgo { get => hallazgo; set => hallazgo = value; }
        public string Propuesta { get => propuesta; set => propuesta = value; }
    }
}
