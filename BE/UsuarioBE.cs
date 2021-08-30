using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BE
{
    public class UsuarioBE:EntityBE
    {
        public UsuarioBE() { }

        public UsuarioBE(CredencialBE cred) 
        
        {
            this.credencial = cred;
        }



        private string nombre;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [DisplayName("Nombre del Usuario")]       
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;
        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private int intentosFallidos;

        public int IntentosFallidos 
        { 
            get { return intentosFallidos; }
            set { intentosFallidos = value; }
        }

        private CredencialBE credencial;
        public CredencialBE Credencial 
        
        { 
            get { return credencial; }
            set { credencial = value; }
        }

        private IdiomaBE idioma;

        public IdiomaBE Idioma 
        
        {
            get { return idioma; }
            set { idioma = value; }
        }

        private bool activo;

        public bool Activo 
        
        { 
            get { return activo; }
            set { activo = value; }
        }


    }
}
