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
    public class UbicacionMPP
    {
        Acceso AccesoDB = new Acceso();


        public UbicacionBE ObtenerUno(UbicacionBE Ubicacion)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Ubicacion.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("UbicacionObtenerPorId", Param);


            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Ubicacion.Calle = Item["Calle"].ToString().Trim();
                    Ubicacion.Altura = Item["Altura"].ToString().Trim();
                    Ubicacion.CodigoPostal = Item["CodigoPostal"].ToString().Trim();
                    Ubicacion.Piso = Item["Piso"].ToString().Trim();
                    Ubicacion.Departamento = Item["NumDepartamento"].ToString().Trim();


                    if ((Item["FechaCreacion"]) != DBNull.Value) { Ubicacion.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Ubicacion.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Ubicacion;

        }

        public void Editar(UbicacionBE Ubicacion)

        {
            try
            {
                string Consulta = "UbicacionEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Ubicacion.Id);
                Parametros.Add("@Calle", Ubicacion.Calle);
                Parametros.Add("@Altura", Ubicacion.Altura);
                Parametros.Add("@CodigoPostal", Ubicacion.CodigoPostal);
                Parametros.Add("@Piso", Ubicacion.Piso);
                Parametros.Add("@Departamento", Ubicacion.Departamento);

                Parametros.Add("@FechaModificacion", Ubicacion.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Ubicacion.UsuarioModificacion.Id);


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
