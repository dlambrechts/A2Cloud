using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class IdiomaBE:EntityBE
    {
        public IdiomaBE() { }

        public IdiomaBE(string _descripcion) 
        {
            
        }

        private string descripcion;
        
        public string Descripcion 
        { 
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}
