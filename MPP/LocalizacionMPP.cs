using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Collections;
using GestorDeArchivo;
using System.Data;

namespace MPP
{
   public class LocalizacionMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(LocalizacionBE Localizacion)

        {
            try
            {
                string Consulta = "LocalizacionInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Descripcion", Localizacion.Descripcion);


                Parametros.Add("@Calle", Localizacion.Ubicacion.Calle);
                Parametros.Add("@Altura", Localizacion.Ubicacion.Altura);
                Parametros.Add("@CodigoPostal", Localizacion.Ubicacion.CodigoPostal);
                Parametros.Add("@Piso", Localizacion.Ubicacion.Piso);

                Parametros.Add("@UsuarioCreacion", Localizacion.UsuarioCreacion.Id);
                Parametros.Add("@FechaCreacion", Localizacion.FechaCreacion);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public List<LocalizacionBE> Listar()

        {
            try
            {
                List<LocalizacionBE> Lista = new List<LocalizacionBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("LocalizacionListar", null);

                DepartamentoMPP mppDepartamento = new DepartamentoMPP();
                PerfilDeHardwareMPP mppPerfil = new PerfilDeHardwareMPP();

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        LocalizacionBE Localizacion = new LocalizacionBE();

                        Localizacion.Id = Convert.ToInt32(Item["Id"]);
                        Localizacion.Descripcion = Item["Descripcion"].ToString().Trim();


                        if ((Item["FechaCreacion"]) != DBNull.Value) { Localizacion.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) Localizacion.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(Localizacion);
                    }

                    return Lista;
                }
                else
                {
                    return Lista;
                }

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return null;
            }
        }

        public LocalizacionBE ObtenerUno(LocalizacionBE Localizacion)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Localizacion.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("LocalizacionObtenerPorId", Param);

            DepartamentoMPP mppDepartamento = new DepartamentoMPP();
            PerfilDeHardwareMPP mppPerfil = new PerfilDeHardwareMPP();
            UbicacionMPP mppUbicacion = new UbicacionMPP();


            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Localizacion.Descripcion = Item["Descripcion"].ToString().Trim();

                    Localizacion.Ubicacion.Id = Convert.ToInt32(Item["Ubicacion"]);
                    Localizacion.Ubicacion = mppUbicacion.ObtenerUno(Localizacion.Ubicacion);


                    if ((Item["FechaCreacion"]) != DBNull.Value) { Localizacion.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Localizacion.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Localizacion;

        }

        public void Editar(LocalizacionBE Localizacion)

        {
            try
            {
                string Consulta = "LocalizacionEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Localizacion.Id);
                Parametros.Add("@Nombre", Localizacion.Descripcion);


                Parametros.Add("@FechaModificacion", Localizacion.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Localizacion.UsuarioModificacion.Id);

                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

                Localizacion.Ubicacion.UsuarioModificacion = new UsuarioBE();
                Localizacion.Ubicacion.UsuarioModificacion.Id = Localizacion.UsuarioModificacion.Id;
                Localizacion.Ubicacion.FechaModificacion = Localizacion.FechaModificacion;

                UbicacionMPP mppUbicacion = new UbicacionMPP();

                mppUbicacion.Editar(Localizacion.Ubicacion);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }

        public void Eliminar(LocalizacionBE Localizacion)

        {
            try
            {
                string Consulta = "LocalizacionEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Localizacion.Id);
                Parametros.Add("@FechaModificacion", Localizacion.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Localizacion.UsuarioModificacion.Id);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }

    }
}
