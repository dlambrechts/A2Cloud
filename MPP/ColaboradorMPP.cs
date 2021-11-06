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
    public class ColaboradorMPP
    {

        Acceso AccesoDB = new Acceso();

        public int Insertar(ColaboradorBE Colaborador)

        {
            try
            {
                string Consulta = "ColaboradorInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Nombre", Colaborador.Nombre);
                Parametros.Add("@Apellido", Colaborador.Apellido);
                Parametros.Add("@Mail", Colaborador.Mail);
                Parametros.Add("@Departamento", Colaborador.Departamento.Id);
                Parametros.Add("@PerfilHardware", Colaborador.PerfilHardware.Id);
                Parametros.Add("@FullRemoto", Colaborador.FullRemoto);

                if (Colaborador.Localizacion != null) { Parametros.Add("@Localizacion", Colaborador.Localizacion.Id); }
                

                Parametros.Add("@Calle", Colaborador.Ubicacion.Calle);
                Parametros.Add("@Altura", Colaborador.Ubicacion.Altura);
                Parametros.Add("@CodigoPostal", Colaborador.Ubicacion.CodigoPostal);
                Parametros.Add("@Piso", Colaborador.Ubicacion.Piso);
                Parametros.Add("@NumDepartamento", Colaborador.Ubicacion.Departamento);

                Parametros.Add("@UsuarioCreacion", Colaborador.UsuarioCreacion.Id);
                Parametros.Add("@FechaCreacion", Colaborador.FechaCreacion);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }



        public List<ColaboradorBE> Listar()

        {
            try
            {
                List<ColaboradorBE> Lista = new List<ColaboradorBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("ColaboradorListar", null);

                DepartamentoMPP mppDepartamento = new DepartamentoMPP();
                PerfilDeHardwareMPP mppPerfil = new PerfilDeHardwareMPP();
                LocalizacionMPP mppLocalizacion = new LocalizacionMPP();
                UbicacionMPP mppUbicacion = new UbicacionMPP();

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        ColaboradorBE Colaborador = new ColaboradorBE();

                        Colaborador.Id = Convert.ToInt32(Item["Id"]);
                        Colaborador.Nombre= Item["Nombre"].ToString().Trim();
                        Colaborador.Apellido = Item["Apellido"].ToString().Trim();
                        Colaborador.Mail = Item["Mail"].ToString().Trim();
                        Colaborador.Departamento.Id = Convert.ToInt32(Item["Departamento"]);
                        Colaborador.Departamento = mppDepartamento.ObtenerUno(Colaborador.Departamento);
                        Colaborador.PerfilHardware.Id = Convert.ToInt32(Item["PerfilDeHardware"]);
                        Colaborador.PerfilHardware = mppPerfil.ObtenerUno(Colaborador.PerfilHardware);

                        if ((Item["Localizacion"]) != DBNull.Value) { 
                        Colaborador.Localizacion.Id = Convert.ToInt32(Item["Localizacion"]);
                        Colaborador.Localizacion = mppLocalizacion.ObtenerUno(Colaborador.Localizacion);
                        }
                        Colaborador.FullRemoto = Convert.ToBoolean(Item["FullRemoto"]);

                        Colaborador.Ubicacion.Id = Convert.ToInt32(Item["Ubicacion"]);
                        Colaborador.Ubicacion = mppUbicacion.ObtenerUno(Colaborador.Ubicacion);

                        if ((Item["FechaCreacion"]) != DBNull.Value) { Colaborador.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) Colaborador.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(Colaborador);
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

        public ColaboradorBE ObtenerUno(ColaboradorBE Colaborador)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Colaborador.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("ColaboradorObtenerPorId", Param);

            DepartamentoMPP mppDepartamento = new DepartamentoMPP();
            PerfilDeHardwareMPP mppPerfil = new PerfilDeHardwareMPP();
            UbicacionMPP mppUbicacion = new UbicacionMPP();
            LocalizacionMPP mppLocalizacion = new LocalizacionMPP();


            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                   
                    Colaborador.Nombre = Item["Nombre"].ToString().Trim();
                    Colaborador.Apellido = Item["Apellido"].ToString().Trim();
                    Colaborador.Mail = Item["Mail"].ToString().Trim();
                    Colaborador.Departamento.Id = Convert.ToInt32(Item["Departamento"]);
                    Colaborador.Departamento = mppDepartamento.ObtenerUno(Colaborador.Departamento);
                    Colaborador.PerfilHardware.Id = Convert.ToInt32(Item["PerfilDeHardware"]);
                    Colaborador.PerfilHardware = mppPerfil.ObtenerUno(Colaborador.PerfilHardware);
                    Colaborador.Ubicacion.Id = Convert.ToInt32(Item["Ubicacion"]);
                    Colaborador.Ubicacion = mppUbicacion.ObtenerUno(Colaborador.Ubicacion);
                    if ((Item["Localizacion"]) != DBNull.Value)
                    {
                        Colaborador.Localizacion.Id = Convert.ToInt32(Item["Localizacion"]);
                        Colaborador.Localizacion = mppLocalizacion.ObtenerUno(Colaborador.Localizacion);
                    }
                    Colaborador.FullRemoto = Convert.ToBoolean(Item["FullRemoto"]);

                    Colaborador.Ubicacion.Id = Convert.ToInt32(Item["Ubicacion"]);
                    Colaborador.Ubicacion = mppUbicacion.ObtenerUno(Colaborador.Ubicacion);

                    if ((Item["FechaCreacion"]) != DBNull.Value) { Colaborador.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Colaborador.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Colaborador;

        }

        public void Editar(ColaboradorBE Colaborador)

        {
            try
            {
                string Consulta = "ColaboradorEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Colaborador.Id);
                Parametros.Add("@Nombre", Colaborador.Nombre);
                Parametros.Add("@Apellido", Colaborador.Apellido);
                Parametros.Add("@Mail", Colaborador.Mail);
                Parametros.Add("@Departamento", Colaborador.Departamento.Id);
                Parametros.Add("@PerfilHardware", Colaborador.PerfilHardware.Id);
                Parametros.Add("@FullRemoto", Colaborador.FullRemoto);
                if (Colaborador.Localizacion != null) { Parametros.Add("@Localizacion", Colaborador.Localizacion.Id); }

                Parametros.Add("@FechaModificacion", Colaborador.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Colaborador.UsuarioModificacion.Id);
               
                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

                Colaborador.Ubicacion.UsuarioModificacion = new UsuarioBE();
                Colaborador.Ubicacion.UsuarioModificacion.Id = Colaborador.UsuarioModificacion.Id;
                Colaborador.Ubicacion.FechaModificacion = Colaborador.FechaModificacion;

                UbicacionMPP mppUbicacion = new UbicacionMPP();

                mppUbicacion.Editar(Colaborador.Ubicacion);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }

        public void Eliminar(ColaboradorBE Colaborador)

        {
            try
            {
                string Consulta = "ColaboradorEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Colaborador.Id);
                Parametros.Add("@FechaModificacion", Colaborador.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Colaborador.UsuarioModificacion.Id);


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

