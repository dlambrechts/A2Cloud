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

        public List<AsignacionActivoBE> Listar()

        {
            try
            {
                List<AsignacionActivoBE> Lista = new List<AsignacionActivoBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("AsignacionActivoListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        AsignacionActivoBE AsignacionActivo = new AsignacionActivoBE();

                        AsignacionActivo.Id = Convert.ToInt32(Item["Id"]);
                        AsignacionActivo.Detalle = Item["Detalle"].ToString().Trim();
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
                    if ((Item["FechaCreacion"]) != DBNull.Value) { AsignacionEstado.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) AsignacionEstado.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return AsignacionEstado;

        }
    }
}
