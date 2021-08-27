using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using DAL;
using BE;

namespace MPP
{
    public class UsuarioMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(UsuarioBE Usuario)

        {
            string Consulta = "UsuarioInsertar";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Nombre", Usuario.Nombre);
            Parametros.Add("@Apellido", Usuario.Apellido);
            Parametros.Add("@Mail", Usuario.Credencial.Mail);
            Parametros.Add("@Idioma", Usuario.Idioma.Id);
            Parametros.Add("@Contraseña", Usuario.Credencial.Contraseña);
         
            return AccesoDB.Escribir(Consulta, Parametros);
        }


        public List<UsuarioBE> ListarUsuarios()

        {
            List<UsuarioBE> ListaUsuarios = new List<UsuarioBE>();

            Acceso AccesoDB = new Acceso();
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("UsuarioListarTodos", null);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    UsuarioBE oUsuario = new UsuarioBE();
                    CredencialBE oCred = new CredencialBE();
                    oUsuario.Credencial = oCred;
                    oUsuario.Id = Convert.ToInt32(Item["Id"]);
                    oUsuario.Nombre = Item["Nombre"].ToString().Trim();
                    oUsuario.Apellido = Item["Apellido"].ToString().Trim();
                    oUsuario.Credencial.Mail = Item["Mail"].ToString().Trim();


                    ListaUsuarios.Add(oUsuario);
                }

                return ListaUsuarios;
            }
            else
            {
                return null;
            }
        }

        public bool ValidarExistencia(CredencialBE cred)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Mail", cred.Mail);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("UsuarioValidarExistencia", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
