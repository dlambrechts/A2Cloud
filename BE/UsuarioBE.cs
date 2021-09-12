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


        private string nombre;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [DisplayName("Nombre")]       
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

        public UsuarioBE() { permisos = new List<PerfilComponenteBE>(); }

        public UsuarioBE(CredencialBE cred) {
           
            credencial = cred; }


        private string digitoHorizontal;

        public string DigitoHorizontal

        {
            get { return digitoHorizontal; }
            set { digitoHorizontal = value; }
        }


        List<PerfilComponenteBE> permisos;
        public List<PerfilComponenteBE> Permisos { get { return permisos; } }
        public void AgregarPermiso(PerfilComponenteBE Perm) { permisos.Add(Perm); }
        public void QuitarPermiso(PerfilComponenteBE Perm) { permisos.Remove(permisos.Where(permisos => permisos.Id == Perm.Id).First()); }
    }
}
