using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CredencialBE
    {
        public CredencialBE() { }

        private string mail;
        public string Mail 
        { 
            get { return mail; }
            set { mail = value; }
        }

        private string contraseña;

        public string Contraseña 
        { 
            get { return contraseña; }
            set { contraseña = value; }
        }
    }
}
