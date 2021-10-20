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
    public class PerfilDeHardwareMPP
    {

        Acceso AccesoDB = new Acceso();
        public PerfilDeHardwareBE ObtenerUno(PerfilDeHardwareBE PerfilDeHardware)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", PerfilDeHardware.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilDeHardwareObtenerPorId", Param);


            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    PerfilDeHardware.Descripcion = Item["Descripcion"].ToString().Trim();

                    if ((Item["FechaCreacion"]) != DBNull.Value) { PerfilDeHardware.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) PerfilDeHardware.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return PerfilDeHardware;

        }

        public List<PerfilDeHardwareBE> Listar()

        {
            try
            {
                List<PerfilDeHardwareBE> Lista = new List<PerfilDeHardwareBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("PerfilDeHardwareListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        PerfilDeHardwareBE PerfilDeHardware = new PerfilDeHardwareBE();

                        PerfilDeHardware.Id = Convert.ToInt32(Item["Id"]);
                        PerfilDeHardware.Descripcion = Item["Descripcion"].ToString().Trim();
                        if ((Item["FechaCreacion"]) != DBNull.Value) { PerfilDeHardware.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) PerfilDeHardware.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(PerfilDeHardware);
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
    }
}
