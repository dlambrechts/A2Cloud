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
    public class AsignacionLicenciaMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(AsignacionLicenciaBE Asignacion)

        {
            try
            {
                string Consulta = "AsignacionLicenciaInsertar";
                Hashtable Parametros = new Hashtable();

                if (Asignacion.Activo.Id != 0) { Parametros.Add("@Activo", Asignacion.Activo.Id); }

                if (Asignacion.Colaborador.Id !=0) { Parametros.Add("@Colaborador", Asignacion.Colaborador.Id); }

                
                Parametros.Add("@Detalle", Asignacion.Detalle);
                
                Parametros.Add("@Licencia", Asignacion.Licencia.Id);
                Parametros.Add("@Estado", Asignacion.Estado.Id);
                Parametros.Add("@FechaInicio", Asignacion.FechaInicio);
                Parametros.Add("@FechaCreacion", Asignacion.FechaCreacion);
                Parametros.Add("@UsuarioCreacion", Asignacion.UsuarioCreacion.Id);
                Parametros.Add("@CantidadAsignada", Asignacion.CantidadAsignada);


                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public int Finalizar(AsignacionLicenciaBE Asignacion)

        {
            try
            {
                string Consulta = "AsignacionLicenciaFinalizar";
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
        public List<AsignacionLicenciaBE> Listar()

        {
            try
            {
                List<AsignacionLicenciaBE> Lista = new List<AsignacionLicenciaBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("AsignacionLicenciaListar", null);

                ActivoMPP mppActivo = new ActivoMPP();
                ColaboradorMPP mppColaborador = new ColaboradorMPP();
                AsignacionActivoMPP mppAsignacionActivo = new AsignacionActivoMPP();
                LicenciaMPP mppLicencia = new LicenciaMPP();

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        AsignacionLicenciaBE AsignacionLicencia = new AsignacionLicenciaBE();

                        AsignacionLicencia.Id = Convert.ToInt32(Item["Id"]);
                        AsignacionLicencia.Detalle = Item["Detalle"].ToString().Trim();

                        if ((Item["Activo"]) != DBNull.Value)
                        {
                            AsignacionLicencia.Activo.Id = Convert.ToInt32(Item["Activo"]);
                            AsignacionLicencia.Activo = mppActivo.ObtenerPorId(AsignacionLicencia.Activo);
                        }

                        if ((Item["Colaborador"]) != DBNull.Value)
                        {
                            AsignacionLicencia.Colaborador.Id = Convert.ToInt32(Item["Colaborador"]);
                            AsignacionLicencia.Colaborador = mppColaborador.ObtenerUno(AsignacionLicencia.Colaborador);

                        }

                        AsignacionLicencia.Licencia.Id = Convert.ToInt32(Item["Licencia"]);
                        AsignacionLicencia.Licencia = mppLicencia.ObtenerUno(AsignacionLicencia.Licencia);

                        AsignacionLicencia.Estado.Id = Convert.ToInt32(Item["Estado"]);
                        AsignacionLicencia.Estado = mppAsignacionActivo.EstadoObtenerUno(AsignacionLicencia.Estado);
                        AsignacionLicencia.FechaInicio = Convert.ToDateTime(Item["FechaInicio"]);

                        if ((Item["FechaCreacion"]) != DBNull.Value) { AsignacionLicencia.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) AsignacionLicencia.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(AsignacionLicencia);
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

        public AsignacionLicenciaBE ObtenerUno(AsignacionLicenciaBE Asignacion)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Asignacion.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("AsignacionLicenciaObtenerPorId", Param);


            ActivoMPP mppActivo = new ActivoMPP();
            ColaboradorMPP mppColaborador = new ColaboradorMPP();
            AsignacionActivoMPP mppAsignacionActivo = new AsignacionActivoMPP();
            LicenciaMPP mppLicencia = new LicenciaMPP();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Asignacion.Detalle = Item["Detalle"].ToString().Trim();
                    if ((Item["Activo"]) != DBNull.Value)
                    {
                        Asignacion.Activo.Id = Convert.ToInt32(Item["Activo"]);
                        Asignacion.Activo = mppActivo.ObtenerPorId(Asignacion.Activo);
                    }

                    if ((Item["Colaborador"]) != DBNull.Value)
                    {
                        Asignacion.Colaborador.Id = Convert.ToInt32(Item["Colaborador"]);
                        Asignacion.Colaborador = mppColaborador.ObtenerUno(Asignacion.Colaborador);

                    }

                    Asignacion.Licencia.Id = Convert.ToInt32(Item["Licencia"]);
                    Asignacion.Licencia = mppLicencia.ObtenerUno(Asignacion.Licencia);
                    Asignacion.Estado.Id = Convert.ToInt32(Item["Estado"]);
                    Asignacion.Estado = mppAsignacionActivo.EstadoObtenerUno(Asignacion.Estado);
                    Asignacion.FechaInicio = Convert.ToDateTime(Item["FechaInicio"]);
                    if ((Item["FechaFinalizacion"]) != DBNull.Value) { Asignacion.FechaFinalizacion = Convert.ToDateTime(Item["FechaFinalizacion"]); }
                    if ((Item["FechaCreacion"]) != DBNull.Value) { Asignacion.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Asignacion.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Asignacion;

        }
    }
}
