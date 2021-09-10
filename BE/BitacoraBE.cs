using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
    public class BitacoraBE : EntityBE
    {

        

        private string detalle;

        public string Detalle 
        
        { 
            get { return detalle; }
            set { detalle = value; }
        }

        public BitacoraBE() { }

        public BitacoraBE(UsuarioBE us) 
        
        {
            UsuarioCreacion = new UsuarioBE();
            UsuarioCreacion= us;
        }
    }
}
