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
            usuario.FechaCreacion = DateTime.Now;
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

        public UsuarioBE ObtenerPorMail(CredencialBE credencial)

        {
            return mppUsuario.ObtenerPorMail(credencial);
        }

        public bool ValidarExistencia (CredencialBE cred) 
        
        {
            return mppUsuario.ValidarExistencia(cred);
        }

        public void Editar (UsuarioBE usuario) 
        
        {
            usuario.FechaModificacion = DateTime.Now;
            mppUsuario.Editar(usuario);
        }

        public void Eliminar(UsuarioBE usuario) 
        
        { 
           usuario.FechaModificacion = DateTime.Now;
            mppUsuario.Eliminar(usuario);

        }

        public void IncrementarIntentosFallidos(UsuarioBE Usuario) 
        
        {
            Usuario.IntentosFallidos++;
            mppUsuario.IncrementarIntentosFallidos(Usuario);
        }

        public void BloquarUsuario(UsuarioBE Usuario)
        {            
            mppUsuario.BloquarUsuario(Usuario);
        }

        public void ReiniciarContador(UsuarioBE Usuario) 
        
        {
            mppUsuario.ReiniciarContador(Usuario);
        }

    }
}
