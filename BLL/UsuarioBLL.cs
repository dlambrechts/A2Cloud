using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class UsuarioBLL
    {
        UsuarioMPP mppUsuario = new UsuarioMPP();

        public int Insertar(UsuarioBE usuario) 
        
        {           
            return mppUsuario.Insertar(usuario);
        }

        public List<UsuarioBE> ListarTodos() 
        
        {
            return mppUsuario.ListarUsuarios();
        }

        public UsuarioBE ObtenerUno(UsuarioBE usuario) 
        
        {
            return mppUsuario.ObtenerUno(usuario);
        }

        public bool ValidarExistencia (CredencialBE cred) 
        
        {
            return mppUsuario.ValidarExistencia(cred);
        }

    }
}
