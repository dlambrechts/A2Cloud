using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;
using System.Collections;
using GestorDeArchivo;

namespace MPP
{
   public class AsignacionActivoMPP
    {
        Acceso AccesoDB = new Acceso();


        public int Insertar(AsignacionActivoBE Asignacion)

        {
            try
            {
                string Consulta = "AsignacionActivoInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Activo", Asignacion.Activo.Id);
                Parametros.Add("@Detalle", Asignacion.Detalle);
                Parametros.Add("@Colaborador", Asignacion.Colaborador.Id);
                Parametros.Add("@Estado", Asignacion.Estado.Id);
                Parametros.Add("@Tipo", Asignacion.Tipo.Id);
                Parametros.Add("@Ubicacion", Asignacion.Ubicacion.Id);
                Parametros.Add("@FechaInicio", Asignacion.FechaInicio);
                Parametros.Add("@FechaCreacion", Asignacion.FechaCreacion);
                Parametros.Add("@UsuarioCreacion", Asignacion.UsuarioCreacion.Id);


                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public int Finalizar(AsignacionActivoBE Asignacion)

        {
            try
            {
                string Consulta = "AsignacionActivoFinalizar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Asignacion.Id);
                Parametros.Add("@FechaFinalizacion", Asignacion.FechaFinalizacion);
                Parametros.Add("@FechaModificacion", Asignacion.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Asignacion.UsuarioModificacion.Id);


                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }
        public List<AsignacionActivoBE> Listar()

        {
            try
            {
                List<AsignacionActivoBE> Lista = new List<AsignacionActivoBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("AsignacionActivoListar", null);

                ActivoMPP mppActivo = new ActivoMPP();
                ColaboradorMPP mppColaborador = new ColaboradorMPP();

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        AsignacionActivoBE AsignacionActivo = new AsignacionActivoBE();

                        AsignacionActivo.Id = Convert.ToInt32(Item["Id"]);
                        AsignacionActivo.Detalle = Item["Detalle"].ToString().Trim();
                        AsignacionActivo.Activo.Id = Convert.ToInt32(Item["Activo"]);
                        AsignacionActivo.Activo = mppActivo.ObtenerPorId(AsignacionActivo.Activo);
                        AsignacionActivo.Colaborador.Id = Convert.ToInt32(Item["Colaborador"]);
                        AsignacionActivo.Colaborador = mppColaborador.ObtenerUno(AsignacionActivo.Colaborador);
                        AsignacionActivo.Tipo.Id = Convert.ToInt32(Item["Tipo"]);
                        AsignacionActivo.Tipo = TipoAsignacionObtenerUno(AsignacionActivo.Tipo);
                        AsignacionActivo.Estado.Id = Convert.ToInt32(Item["Estado"]);
                        AsignacionActivo.Estado = EstadoObtenerUno(AsignacionActivo.Estado);
                        AsignacionActivo.FechaInicio = Convert.ToDateTime(Item["FechaInicio"]);

                        if ((Item["FechaCreacion"]) != DBNull.Value) { AsignacionActivo.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) AsignacionActivo.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(AsignacionActivo);
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

        public AsignacionActivoBE ObtenerUno(AsignacionActivoBE Asignacion)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Asignacion.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("AsignacionActivoObtenerPorId", Param);


            ColaboradorMPP mppColaborador = new ColaboradorMPP();
            ActivoMPP mppActivo = new ActivoMPP();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Asignacion.Detalle = Item["Detalle"].ToString().Trim();
                    Asignacion.Activo.Id = Convert.ToInt32(Item["Activo"]);
                    Asignacion.Activo = mppActivo.ObtenerPorId(Asignacion.Activo);
                    Asignacion.Colaborador.Id = Convert.ToInt32(Item["Colaborador"]);
                    Asignacion.Colaborador = mppColaborador.ObtenerUno(Asignacion.Colaborador);
                    Asignacion.Tipo.Id = Convert.ToInt32(Item["Tipo"]);
                    Asignacion.Tipo = TipoAsignacionObtenerUno(Asignacion.Tipo);
                    Asignacion.Estado.Id = Convert.ToInt32(Item["Estado"]);
                    Asignacion.Estado = EstadoObtenerUno(Asignacion.Estado);
                    Asignacion.FechaInicio = Convert.ToDateTime(Item["FechaInicio"]);
                    if ((Item["FechaFinalizacion"]) != DBNull.Value) { Asignacion.FechaFinalizacion = Convert.ToDateTime(Item["FechaFinalizacion"]); }
                    if ((Item["FechaCreacion"]) != DBNull.Value) { Asignacion.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Asignacion.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Asignacion;

        }
        public List<AsignacionEstadoBE> ListarEstados()

        {
            try
            {
                List<AsignacionEstadoBE> Lista = new List<AsignacionEstadoBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("AsignacionEstadoListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        AsignacionEstadoBE AsignacionEstado = new AsignacionEstadoBE();

                        AsignacionEstado.Id = Convert.ToInt32(Item["Id"]);
                        AsignacionEstado.Descripcion = Item["Descripcion"].ToString().Trim();


                        Lista.Add(AsignacionEstado);
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

        public AsignacionEstadoBE EstadoObtenerUno(AsignacionEstadoBE AsignacionEstado)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", AsignacionEstado.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("AsignacionEstadoObtenerPorId", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    AsignacionEstado.Descripcion = Item["Descripcion"].ToString().Trim();


                }
            }

            return AsignacionEstado;

        }


        public List<AsignacionTipoBE> ListarTipoAsignacion()

        {
            try
            {
                List<AsignacionTipoBE> Lista = new List<AsignacionTipoBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("AsignacionTipoListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        AsignacionTipoBE AsignacionTipo = new AsignacionTipoBE();

                        AsignacionTipo.Id = Convert.ToInt32(Item["Id"]);
                        AsignacionTipo.Descripcion = Item["Descripcion"].ToString().Trim();


                        Lista.Add(AsignacionTipo);
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

        public AsignacionTipoBE TipoAsignacionObtenerUno(AsignacionTipoBE AsignacionTipo)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", AsignacionTipo.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("AsignacionTipoObtenerPorId", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    AsignacionTipo.Descripcion = Item["Descripcion"].ToString().Trim();


                }
            }

            return AsignacionTipo;

        }

    }
}
