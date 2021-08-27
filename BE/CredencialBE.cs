using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class CredencialBE
    {
        public CredencialBE() { }

        private string mail;


        [DisplayName("Correo electrónico")]
        public string Mail 
        { 
            get { return mail; }
            set { mail = value; }
        }

        private string contraseña;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contraseña 
        { 
            get { return contraseña; }
            set { contraseña = value; }
        }
    }
}
