using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioBE:EntityBE
    {
        public UsuarioBE() { }

        public UsuarioBE(string _nombre,string _apellido) 
        
        {
            nombre = _nombre;
            apellido = _apellido;
        }

        private string nombre;
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

    }
}
