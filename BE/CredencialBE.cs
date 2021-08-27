using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace BE
{
    public class CredencialBE
    {
        public CredencialBE() { }

        private string mail;

        [Required]
        [EmailAddress]
        [DisplayName("Correo electrónico")]
        public string Mail 
        { 
            get { return mail; }
            set { mail = value; }
        }

        private string contraseña;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MembershipPassword(
        MinRequiredNonAlphanumericCharacters = 1,
        MinNonAlphanumericCharactersError = "La contraseña debe contener al menos un símbolo (!, @, #, etc).",
        ErrorMessage = "La contraseña debe contener al menos 6 caracteres con al menos un símbolo (!, @, #, etc).",
        MinRequiredPasswordLength = 6
        )]
        public string Contraseña 
        { 
            get { return contraseña; }
            set { contraseña = value; }
        }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = "Por favor confirme la contraseña")]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarCont { get; set; }
    }
}
