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
    public class LicenciaMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(LicenciaBE Licencia)

        {
            try
            {
                string Consulta = "LicenciaInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Descripcion", Licencia.Descripcion);
                Parametros.Add("@FechaVigencia", Licencia.FechaVigencia);
                Parametros.Add("@FechaFinalizacion", Licencia.FechaFinalizacion);
                Parametros.Add("@Producto", Licencia.Producto.Id);
                Parametros.Add("@NumeroContrato", Licencia.NumeroContrato);
                Parametros.Add("@Cantidad", Licencia.Cantidad);
                Parametros.Add("@Modalidad", Licencia.Modalidad.Id);
                Parametros.Add("@UsuarioCreacion", Licencia.UsuarioCreacion.Id);
                Parametros.Add("@FechaCreacion", Licencia.FechaCreacion);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public void Editar(LicenciaBE Licencia)

        {
            try
            {
                string Consulta = "LicenciaEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Licencia.Id);
                Parametros.Add("@Descripcion", Licencia.Descripcion);
                Parametros.Add("@FechaVigencia", Licencia.FechaVigencia);
                Parametros.Add("@FechaFinalizacion", Licencia.FechaFinalizacion);
                Parametros.Add("@Producto", Licencia.Producto.Id);
                Parametros.Add("@NumeroContrato", Licencia.NumeroContrato);
                Parametros.Add("@Cantidad", Licencia.Cantidad);
                Parametros.Add("@Modalidad", Licencia.Modalidad.Id);
                Parametros.Add("@UsuarioModificacion", Licencia.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", Licencia.FechaModificacion);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }


        public List<LicenciaBE> Listar()

        {
            try
            {
                List<LicenciaBE> Lista = new List<LicenciaBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("LicenciaListar", null);

                ProductoSoftwareMPP softMPP = new ProductoSoftwareMPP();

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        LicenciaBE Licencia = new LicenciaBE();

                        Licencia.Id = Convert.ToInt32(Item["Id"]);
                        Licencia.Descripcion = Item["Descripcion"].ToString().Trim();
                        if ((Item["FechaVigencia"]) != DBNull.Value) { Licencia.FechaVigencia = Convert.ToDateTime(Item["FechaVigencia"]); }
                        if ((Item["FechaFinalizacion"]) != DBNull.Value) { Licencia.FechaFinalizacion = Convert.ToDateTime(Item["FechaFinalizacion"]); }
                        Licencia.Producto.Id = Convert.ToInt32(Item["Producto"]);
                        Licencia.Producto = softMPP.ObtenerUno(Licencia.Producto);
                        Licencia.Cantidad = Convert.ToInt32(Item["Cantidad"]);
                        Licencia.Modalidad.Id = Item["Modalidad"].ToString().Trim();
                        Licencia.Modalidad = ModalidadObtenerUno(Licencia.Modalidad);
                        Licencia.NumeroContrato = Item["NumeroContrato"].ToString().Trim();

                        if ((Item["FechaCreacion"]) != DBNull.Value) { Licencia.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) Licencia.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(Licencia);
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

        public LicenciaBE ObtenerUno(LicenciaBE Licencia)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Licencia.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("LicenciaObtenerPorId", Param);

            ProductoSoftwareMPP softMPP = new ProductoSoftwareMPP();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Licencia.Descripcion = Item["Descripcion"].ToString().Trim();
                    if ((Item["FechaVigencia"]) != DBNull.Value) { Licencia.FechaVigencia = Convert.ToDateTime(Item["FechaVigencia"]); }
                    if ((Item["FechaFinalizacion"]) != DBNull.Value) { Licencia.FechaFinalizacion = Convert.ToDateTime(Item["FechaFinalizacion"]); }
                    Licencia.Producto.Id = Convert.ToInt32(Item["Producto"]);
                    Licencia.Producto = softMPP.ObtenerUno(Licencia.Producto);
                    Licencia.Cantidad = Convert.ToInt32(Item["Cantidad"]);
                    Licencia.Modalidad.Id = Item["Modalidad"].ToString().Trim();
                    Licencia.Modalidad = ModalidadObtenerUno(Licencia.Modalidad);
                    Licencia.NumeroContrato = Item["NumeroContrato"].ToString().Trim();

                    if ((Item["FechaCreacion"]) != DBNull.Value) { Licencia.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Licencia.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Licencia;

        }

        public void Eliminar(LicenciaBE Licencia)

        {
            try
            {
                string Consulta = "LicenciaEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Licencia.Id);
                Parametros.Add("@FechaModificacion", Licencia.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Licencia.UsuarioModificacion.Id);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }




        public LicenciaModalidadBE ModalidadObtenerUno(LicenciaModalidadBE Modalidad)
        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Modalidad.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("LicenciaModalidadObtenerPorId", Param);


            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Modalidad.Descripcion = Item["Descripcion"].ToString().Trim();


                }
            }

            return Modalidad;

        }


        public List<LicenciaModalidadBE> ModalidadListar()

        {
            try
            {
                List<LicenciaModalidadBE> Lista = new List<LicenciaModalidadBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("LicenciaModalidadListar", null);


                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        LicenciaModalidadBE LicenciaModalidad = new LicenciaModalidadBE();

                        LicenciaModalidad.Id = Item["Id"].ToString().Trim();
                        LicenciaModalidad.Descripcion = Item["Descripcion"].ToString().Trim();


                        Lista.Add(LicenciaModalidad);
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
