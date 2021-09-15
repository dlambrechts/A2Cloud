using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class IdiomaTraduccionBE : EntityBE
    {
        private IdiomaEtiquetaBE etiqueta;

        public IdiomaEtiquetaBE Etiqueta

        {
            get { return etiqueta; }
            set { etiqueta = value; }
        }

        private string texto;

        public string Texto

        {
            get { return texto; }
            set { texto = value; }
        }

        public IdiomaTraduccionBE()
        
        {
            etiqueta = new IdiomaEtiquetaBE();
        }


    }
}
