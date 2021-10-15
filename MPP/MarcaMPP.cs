using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using GestorDeArchivo;
using System.Collections;
using System.Data;

namespace MPP
{
    public class MarcaMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(MarcaBE Marca)

        {
            try
            {
                string Consulta = "MarcaInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Descripcion", Marca.Descripcion);
                Parametros.Add("@FechaCreacion", Marca.FechaCreacion);
                Parametros.Add("@UsuarioCreacion", Marca.UsuarioCreacion.Id);


                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public void Editar(MarcaBE Marca)

        {
            try
            {
                string Consulta = "MarcaEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Marca.Id);
                Parametros.Add("@Descripcion", Marca.Descripcion);
                Parametros.Add("@FechaModificacion", Marca.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Marca.UsuarioModificacion.Id);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }

        public List<MarcaBE> Listar()

        {
            try
            {
                List<MarcaBE> Lista = new List<MarcaBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("MarcaListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        MarcaBE marca = new MarcaBE();

                        marca.Id = Convert.ToInt32(Item["Id"]);
                        marca.Descripcion = Item["Descripcion"].ToString().Trim();
                        if ((Item["FechaCreacion"]) != DBNull.Value) { marca.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) marca.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);


                        Lista.Add(marca);
                    }

                    return Lista;
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

        public MarcaBE ObtenerUno(MarcaBE Marca)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Marca.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("MarcaObtenerPorId", Param);          

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Marca.Descripcion = Item["Descripcion"].ToString().Trim();
                    if ((Item["FechaCreacion"]) != DBNull.Value) { Marca.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Marca.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Marca;

        }

        public void Eliminar(MarcaBE Marca)

        {
            try
            {
                string Consulta = "MarcaEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Marca.Id);
                Parametros.Add("@FechaModificacion", Marca.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Marca.UsuarioModificacion.Id);


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
