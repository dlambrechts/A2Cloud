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
    public class ProductoSoftwareMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(ProductoSoftwareBE ProductoSoftware)

        {
            try
            {
                string Consulta = "ProductoSoftwareInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Descripcion", ProductoSoftware.Descripcion);
                Parametros.Add("@Marca", ProductoSoftware.Marca.Id);
                Parametros.Add("@FechaCreacion", ProductoSoftware.FechaCreacion);
                Parametros.Add("@UsuarioCreacion", ProductoSoftware.UsuarioCreacion.Id);


                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public void Editar(ProductoSoftwareBE ProductoSoftware)

        {
            try
            {
                string Consulta = "ProductoSoftwareEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", ProductoSoftware.Id);
                Parametros.Add("@Descripcion", ProductoSoftware.Descripcion);
                Parametros.Add("@Marca", ProductoSoftware.Marca.Id);
                Parametros.Add("@FechaModificacion", ProductoSoftware.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", ProductoSoftware.UsuarioModificacion.Id);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }

        public List<ProductoSoftwareBE> Listar()

        {
            try
            {
                List<ProductoSoftwareBE> Lista = new List<ProductoSoftwareBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("ProductoSoftwareListar", null);

                MarcaMPP mppMarca = new MarcaMPP();

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        ProductoSoftwareBE ProductoSoftware = new ProductoSoftwareBE();

                        ProductoSoftware.Id = Convert.ToInt32(Item["Id"]);
                        ProductoSoftware.Descripcion = Item["Descripcion"].ToString().Trim();
                        ProductoSoftware.Marca.Id = Convert.ToInt32(Item["Marca"]);
                        ProductoSoftware.Marca = mppMarca.ObtenerUno(ProductoSoftware.Marca);
                        if ((Item["FechaCreacion"]) != DBNull.Value) { ProductoSoftware.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) ProductoSoftware.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(ProductoSoftware);
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

        public ProductoSoftwareBE ObtenerUno(ProductoSoftwareBE ProductoSoftware)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", ProductoSoftware.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("ProductoSoftwareObtenerPorId", Param);

            MarcaMPP mppMarca = new MarcaMPP();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    ProductoSoftware.Descripcion = Item["Descripcion"].ToString().Trim();
                    ProductoSoftware.Marca.Id = Convert.ToInt32(Item["Marca"]);
                    ProductoSoftware.Marca = mppMarca.ObtenerUno(ProductoSoftware.Marca);
                    if ((Item["FechaCreacion"]) != DBNull.Value) { ProductoSoftware.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) ProductoSoftware.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return ProductoSoftware;

        }

        public void Eliminar(ProductoSoftwareBE ProductoSoftware)

        {
            try
            {
                string Consulta = "ProductoSoftwareEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", ProductoSoftware.Id);
                Parametros.Add("@FechaModificacion", ProductoSoftware.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", ProductoSoftware.UsuarioModificacion.Id);


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
