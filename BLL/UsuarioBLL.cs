using System;
using System.Collections.Generic;
using System.Security;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Seguridad;
using MPP;
using BE;
using System.Web.Security;

namespace BLL
{
    public class UsuarioBLL
    {
        UsuarioMPP mppUsuario = new UsuarioMPP();
        DigitoVerificadorBLL dvBLL = new DigitoVerificadorBLL();
       
        public void Insertar(UsuarioBE usuario) 
        
        {   
            
            usuario.FechaCreacion = DateTime.Now;
            usuario.DigitoHorizontal = dvBLL.CalcularDigitoHorizontal(usuario);

            mppUsuario.Insertar(usuario);

            string Dvv = dvBLL.CalcularDigitoVertical(ListarTodos());
            dvBLL.ActualizarDigitoVertical(Dvv);

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

        public void CambiarContraseña(UsuarioBE Usuario)
        {
            Usuario.FechaModificacion = DateTime.Now;
            mppUsuario.CambiarContraseña(Usuario);

        }

        public void RecuperarContraseña(UsuarioBE Usuario)
        {

            string NuevaContraseña = Membership.GeneratePassword(8, 1);
            Usuario.Credencial.Contraseña = Encriptado.Hash(NuevaContraseña);

            Usuario.FechaModificacion = DateTime.Now;
            CambiarContraseña(Usuario);


            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("a2cloud@outlook.com");
            message.To.Add(new MailAddress(Usuario.Credencial.Mail));
            message.Subject = "Recuperar Contraseña - A2Cloud";
            message.IsBodyHtml = true;
            message.Body = @"<html>
                      <body>
                      <h4>Se ha generado una solicitud de cambio de contraseña</h4>
                      <p>Su nueva contraseña es: " + NuevaContraseña + @"</p>
                      <p>Por favor, cambie su contraseña una vez que ingrese a la plataforma</br></p>
                      <p>A2Cloud</br></p>
                      </body>
                      </html>
                     ";
            smtp.Port = 587;
            smtp.Host = "smtp.live.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("a2cloud@outlook.com", "Uai2021!!");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);


        }

    }
}
