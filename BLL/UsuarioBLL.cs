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
        DigitoVerificadorBLL dvBLL = new DigitoVerificadorBLL();
       
        public int Insertar(UsuarioBE usuario) 
        
        {
            usuario.FechaCreacion = DateTime.Now;
            usuario.DigitoHorizontal = dvBLL.CalcularDigitoHorizontal(usuario);

            string Dvv = dvBLL.CalcularDigitoVertical(ListarTodos());
            dvBLL.ActualizarDigitoVertical(Dvv);

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
            usuario.DigitoHorizontal = dvBLL.CalcularDigitoHorizontal(usuario);
            mppUsuario.Editar(usuario);
            string Dvv= dvBLL.CalcularDigitoVertical(ListarTodos());
            dvBLL.ActualizarDigitoVertical(Dvv);
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

        public void GuardarPerfil(UsuarioBE Usuario)

        {
            mppUsuario.GuardarPefil(Usuario);
        }
    }
}
