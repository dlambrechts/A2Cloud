using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using DAL;
using BE;
using GestorDeArchivo;

namespace MPP
{
    public class UsuarioMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(UsuarioBE Usuario)

        {
            try { 
            string Consulta = "UsuarioInsertar";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Nombre", Usuario.Nombre);
            Parametros.Add("@Apellido", Usuario.Apellido);
            Parametros.Add("@Mail", Usuario.Credencial.Mail);
            Parametros.Add("@Idioma", Usuario.Idioma.Id);
            Parametros.Add("@Contraseña", Usuario.Credencial.Contraseña);
            Parametros.Add("@UsuarioCreacion", Usuario.UsuarioCreacion.Id);
            Parametros.Add("@FechaCreacion", Usuario.FechaCreacion);
            Parametros.Add("@Dvh", Usuario.DigitoHorizontal);

            return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch(Exception ex)

            {

                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public void Editar(UsuarioBE Usuario)

        {
            try
            {
                string Consulta = "UsuarioEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Usuario.Id);
                Parametros.Add("@Nombre", Usuario.Nombre);
                Parametros.Add("@Apellido", Usuario.Apellido);
                Parametros.Add("@Mail", Usuario.Credencial.Mail);
                Parametros.Add("@Idioma", Usuario.Idioma.Id);
                Parametros.Add("@UsuarioModificacion", Usuario.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", Usuario.FechaModificacion);
                Parametros.Add("@Activo", Usuario.Activo);
                Parametros.Add("@Dvh", Usuario.DigitoHorizontal);

                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);            
            }
        }

        public void CambiarContraseña(UsuarioBE Usuario)

        {
            try { 
            string Consulta = "UsuarioCambiarContraseña";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Usuario", Usuario.Id);
            Parametros.Add("@Contraseña", Usuario.Credencial.Contraseña);
            Parametros.Add("@UsuarioModificacion", Usuario.UsuarioModificacion.Id);
            Parametros.Add("@FechaModificacion", Usuario.FechaModificacion);

            Acceso nAcceso = new Acceso();

            nAcceso.Escribir(Consulta, Parametros);

        }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);            
            }
}



        public List<UsuarioBE> ListarUsuarios()

        {
            try { 
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
                    oUsuario.Activo = Convert.ToBoolean(Item["Activo"]);
                    oUsuario.DigitoHorizontal = Convert.ToString(Item["Dvh"]).Trim();



                    ListaUsuarios.Add(oUsuario);
                }

                return ListaUsuarios;
            }
            else
            {
                return null;
            }

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
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

        public UsuarioBE ObtenerUno (UsuarioBE usuario) 
        
        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", usuario.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("UsuarioObtenerPorId", Param);

            UsuarioBE oUsuario = new UsuarioBE();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    
                    CredencialBE oCred = new CredencialBE();
                    IdiomaBE Idio = new IdiomaBE();
                    oUsuario.Credencial = oCred;
                    oUsuario.Idioma = Idio;
                    oUsuario.Id = Convert.ToInt32(Item["Id"]);
                    oUsuario.Nombre = Item["Nombre"].ToString().Trim();
                    oUsuario.Apellido = Item["Apellido"].ToString().Trim();
                    oUsuario.Credencial.Mail = Item["Mail"].ToString().Trim();
                    oUsuario.Credencial.Contraseña = Item["Contraseña"].ToString().Trim();
                    oUsuario.Idioma.Id= Convert.ToInt32(Item["Idioma"]);
                    oUsuario.Activo = Convert.ToBoolean(Item["Activo"]);
                    oUsuario.IntentosFallidos = Convert.ToInt32(Item["IntentosFallidos"]);
                    if ((Item["FechaCreacion"]) != DBNull.Value) { oUsuario.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) oUsuario.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return oUsuario;

        }

        public UsuarioBE ObtenerPorMail(CredencialBE credencial)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Mail", credencial.Mail);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("UsuarioObtenerPorMail", Param);

            UsuarioBE oUsuario = new UsuarioBE();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    CredencialBE oCred = new CredencialBE();
                    IdiomaBE Idio = new IdiomaBE();
                    oUsuario.Credencial = oCred;
                    oUsuario.Idioma = Idio;
                    oUsuario.Id = Convert.ToInt32(Item["Id"]);
                    oUsuario.Nombre = Item["Nombre"].ToString().Trim();
                    oUsuario.Apellido = Item["Apellido"].ToString().Trim();
                    oUsuario.Credencial.Mail = Item["Mail"].ToString().Trim();
                    oUsuario.Credencial.Contraseña = Item["Contraseña"].ToString().Trim();
                    oUsuario.Idioma.Id = Convert.ToInt32(Item["Idioma"]);
                    oUsuario.Activo = Convert.ToBoolean(Item["Activo"]);
                    oUsuario.IntentosFallidos= Convert.ToInt32(Item["IntentosFallidos"]);
                    if ((Item["FechaCreacion"]) != DBNull.Value) { oUsuario.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) oUsuario.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return oUsuario;

        }



        public void IncrementarIntentosFallidos(UsuarioBE Usuario)

        {
            string Consulta = "UsuarioIncrementarIntentosFallidos";
            Hashtable Parametros = new Hashtable();
            Parametros.Add("@Mail", Usuario.Credencial.Mail);

            Acceso nAcceso = new Acceso();

            nAcceso.Escribir(Consulta, Parametros);
        }

        public void BloquarUsuario(UsuarioBE Usuario)

        {
            string Consulta = "UsuarioBloquear";
            Hashtable Parametros = new Hashtable();
            Parametros.Add("@Mail", Usuario.Credencial.Mail);

            Acceso nAcceso = new Acceso();

            nAcceso.Escribir(Consulta, Parametros);
        }

        public void ReiniciarContador(UsuarioBE Usuario)

        {
            string Consulta = "UsuarioReiniciarContador";
            Hashtable Parametros = new Hashtable();
            Parametros.Add("@Mail", Usuario.Credencial.Mail);

            Acceso nAcceso = new Acceso();

            nAcceso.Escribir(Consulta, Parametros);
        }

        public void Eliminar(UsuarioBE Usuario) 
        
        {
            string Consulta = "UsuarioEliminar";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Id", Usuario.Id);
            Parametros.Add("@FechaModificacion", Usuario.FechaModificacion);
            Acceso nAcceso = new Acceso();

            nAcceso.Escribir(Consulta, Parametros);

        }

        public void GuardarPefil(UsuarioBE Usuario)

        {
      
            string ConsultaDel = "UsuarioPermisosBorrar"; // Primero borro los permisos que tenía el usuario
            Hashtable ParametrosDel = new Hashtable();
            ParametrosDel.Add("IdUsuario", Usuario.Id);
            AccesoDB.Escribir(ConsultaDel, ParametrosDel);

            string ConsultaAdd = "UsuarioPermisosGuardar"; // Luego guardo los nuevos permisos
            Hashtable ParametrosAdd = new Hashtable();
            ParametrosAdd.Add("IdUsuario", Usuario.Id);

            foreach (var item in Usuario.Permisos)
            {

                ParametrosAdd.Add("IdPermiso", item.Id);
                AccesoDB.Escribir(ConsultaAdd, ParametrosAdd);
                ParametrosAdd.Remove("IdPermiso");

            }
        }
    }
}
