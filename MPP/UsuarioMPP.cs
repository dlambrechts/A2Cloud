using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DAL;
using BE;

namespace MPP
{
    public class UsuarioMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(UsuarioBE Usuario)

        {
            string Consulta = "InsertarUsuario";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Nombre", Usuario.Nombre);
            Parametros.Add("@Apellido", Usuario.Apellido);
            Parametros.Add("@Mail", Usuario.Credencial.Mail);
            Parametros.Add("@Password", Usuario.Credencial.Contraseña);
         
            return AccesoDB.Escribir(Consulta, Parametros);
        }
    }
}
