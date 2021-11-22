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
    public class ActivoMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(ActivoBE Activo)

        {
            try
            {
                string Consulta = "ActivoInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Nombre", Activo.Nombre);
                Parametros.Add("@FechaCompra", Activo.FechaCompra);
                Parametros.Add("@Marca", Activo.Marca.Id);
                Parametros.Add("@Modelo", Activo.Modelo);
                Parametros.Add("@CicloDeVida", Activo.CicloDeVida);
                Parametros.Add("@UsuarioCreacion", Activo.UsuarioCreacion.Id);
                Parametros.Add("@FechaCreacion", Activo.FechaCreacion);
                Parametros.Add("@Tipo", Activo.Tipo.Id);
                Parametros.Add("@NumeroSerie", Activo.NumeroSerie);
                string Estado = Activo.Estado.GetType().ToString();
                Estado = Estado.Substring(0, Estado.Length - 2);
                Estado = Estado.Substring(3);
                Parametros.Add("@Estado", Estado);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public int InsertarPc(ActivoBE Activo)

        {
            try
            {
                string Consulta = "ActivoPcInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Nombre", Activo.Nombre);
                Parametros.Add("@FechaCompra", Activo.FechaCompra);
                Parametros.Add("@Marca", Activo.Marca.Id);
                Parametros.Add("@Modelo", Activo.Modelo);
                Parametros.Add("@CicloDeVida", Activo.CicloDeVida);
                Parametros.Add("@NumeroSerie", Activo.NumeroSerie);
                Parametros.Add("@UsuarioCreacion", Activo.UsuarioCreacion.Id);
                Parametros.Add("@FechaCreacion", Activo.FechaCreacion);
                Parametros.Add("@Tipo", Activo.Tipo.Id);
                Parametros.Add("@ModeloProcesador", Activo.ModeloProcesador);
                Parametros.Add("@FrecuenciaProcesador", Activo.FrecuenciaProcesador);
                Parametros.Add("@NucleosProcesador", Activo.NucleosProcesador);
                Parametros.Add("@MemoriaRam", Activo.MemoriaRam);
                Parametros.Add("@MemoriaVideo", Activo.MemoriaVideo);
                Parametros.Add("@AceleradoraGrafica", Activo.AceleradoraGrafica);
                Parametros.Add("@TamañoDisco", Activo.TamañoDisco);

                string Estado = Activo.Estado.GetType().ToString();
                Estado= Estado.Substring(0, Estado.Length - 2);
                Estado = Estado.Substring(3);            
                Parametros.Add("@Estado",Estado );

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public ActivoBE ObtenerPorId(ActivoBE Activo) 
        
        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Activo.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("ActivoObtenerPorId", Param);

            MarcaMPP mppMarca = new MarcaMPP();


            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    Activo.Nombre = Item["Nombre"].ToString().Trim();
                    Activo.FechaCompra = Convert.ToDateTime(Item["FechaCompra"]);
                    Activo.Marca.Id = Convert.ToInt32(Item["Marca"]);
                    Activo.Marca = mppMarca.ObtenerUno(Activo.Marca);
                    Activo.Modelo = Item["Modelo"].ToString().Trim();
                    Activo.CicloDeVida = Convert.ToInt32(Item["CicloDeVida"]);
                    Activo.FechaCompra = Convert.ToDateTime(Item["FechaCompra"]);
                    Activo.Tipo.Id = Convert.ToInt32(Item["Tipo"]);
                    Activo.Tipo = ObtenerTipoPorId(Activo.Tipo);
                    Activo.NumeroSerie = Item["NumeroSerie"].ToString().Trim();

                    if ((Item["AceleradoraGrafica"]) != DBNull.Value) { Activo.AceleradoraGrafica = Convert.ToBoolean(Item["AceleradoraGrafica"]); }
                    if ((Item["MemoriaRam"]) != DBNull.Value) { Activo.MemoriaRam = Convert.ToInt32(Item["MemoriaRam"]); }
                    if ((Item["TamañoDisco"]) != DBNull.Value) { Activo.TamañoDisco = Convert.ToInt32(Item["TamañoDisco"]); }
                    Activo.ModeloProcesador = Item["ModeloProcesador"].ToString().Trim();
                    if ((Item["NucleosProcesador"]) != DBNull.Value) { Activo.NucleosProcesador = Convert.ToInt32(Item["NucleosProcesador"]); }
                    if ((Item["FrecuenciaProcesador"]) != DBNull.Value) { Activo.FrecuenciaProcesador = Convert.ToDecimal(Item["FrecuenciaProcesador"]); }
                    if ((Item["MemoriaVideo"]) != DBNull.Value) { Activo.MemoriaVideo = Convert.ToInt32(Item["MemoriaVideo"]); }

                    if ((Item["FechaCreacion"]) != DBNull.Value) { Activo.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) { Activo.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]); }

                }
            }

            return Activo;

        }


        public List<ActivoBE> Listar()

        {
            try
            {
                List<ActivoBE> Lista = new List<ActivoBE>();
                MarcaMPP mppMarca = new MarcaMPP();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("ActivoListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        ActivoBE Activo = new ActivoBE();

                        Activo.Id = Convert.ToInt32(Item["Id"]);
                        Activo.Nombre = Item["Nombre"].ToString().Trim();
                        Activo.FechaCompra = Convert.ToDateTime(Item["FechaCompra"]);
                        Activo.Marca.Id = Convert.ToInt32(Item["Marca"]);
                        Activo.Marca = mppMarca.ObtenerUno(Activo.Marca);
                        Activo.Modelo = Item["Modelo"].ToString().Trim();
                        Activo.CicloDeVida = Convert.ToInt32(Item["CicloDeVida"]);
                        Activo.Tipo.Id = Convert.ToInt32(Item["Tipo"]);
                        Activo.Tipo = ObtenerTipoPorId(Activo.Tipo);
                        Activo.NumeroSerie = Item["NumeroSerie"].ToString().Trim();

                        if ((Item["AceleradoraGrafica"]) != DBNull.Value) { Activo.AceleradoraGrafica = Convert.ToBoolean(Item["AceleradoraGrafica"]); }
                        if ((Item["MemoriaRam"]) != DBNull.Value) { Activo.MemoriaRam = Convert.ToInt32(Item["MemoriaRam"]); }
                        if ((Item["TamañoDisco"]) != DBNull.Value) { Activo.TamañoDisco = Convert.ToInt32(Item["TamañoDisco"]); }
                        Activo.ModeloProcesador = Item["ModeloProcesador"].ToString().Trim();
                        if ((Item["NucleosProcesador"]) != DBNull.Value) { Activo.NucleosProcesador = Convert.ToInt32(Item["NucleosProcesador"]); }
                        if ((Item["FrecuenciaProcesador"]) != DBNull.Value) { Activo.FrecuenciaProcesador = Convert.ToDecimal(Item["FrecuenciaProcesador"]); }
                        if ((Item["MemoriaVideo"]) != DBNull.Value) { Activo.MemoriaVideo = Convert.ToInt32(Item["MemoriaVideo"]); }
                        if ((Item["FechaCreacion"]) != DBNull.Value) { Activo.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) Activo.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        ActivoEstadoBE estado;
                        switch (Convert.ToString(Item["Estado"]).Trim())

                        {
                            case "ActivoEstadoAsignado": { estado= new ActivoEstadoAsignadoBE(); estado.Codigo = estado.GetType().ToString(); estado.Descripcion = Item["DescEstado"].ToString().Trim(); Activo.CambiarEstado(estado); } break;
                            case "ActivoEstadoDisponible": { estado = new ActivoEstadoDisponibleBE(); estado.Codigo = estado.GetType().ToString(); estado.Descripcion = Item["DescEstado"].ToString().Trim(); Activo.CambiarEstado(estado); } break;
                            case "ActivoEstadoBaja": { estado= new ActivoEstadoBajaBE(); estado.Codigo = estado.GetType().ToString(); estado.Descripcion = Item["DescEstado"].ToString().Trim(); Activo.CambiarEstado(estado); } break;

                        }


                       
                       

                        Lista.Add(Activo);
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

        public int Editar(ActivoBE Activo)

        {
            try
            {
                string Consulta = "ActivoEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Activo.Id);
                Parametros.Add("@Nombre", Activo.Nombre);
                Parametros.Add("@FechaCompra", Activo.FechaCompra);
                Parametros.Add("@Marca", Activo.Marca.Id);
                Parametros.Add("@Modelo", Activo.Modelo);
                Parametros.Add("@CicloDeVida", Activo.CicloDeVida);
                Parametros.Add("@UsuarioModificacion", Activo.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", Activo.FechaModificacion);
                Parametros.Add("@NumeroSerie", Activo.NumeroSerie);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }



        public int EditarPc(ActivoBE Activo)

        {
            try
            {
                string Consulta = "ActivoPcEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Activo.Id);
                Parametros.Add("@Nombre", Activo.Nombre);
                Parametros.Add("@FechaCompra", Activo.FechaCompra);
                Parametros.Add("@Marca", Activo.Marca.Id);
                Parametros.Add("@Modelo", Activo.Modelo);
                Parametros.Add("@CicloDeVida", Activo.CicloDeVida);
                Parametros.Add("@UsuarioModificacion", Activo.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", Activo.FechaModificacion);
                Parametros.Add("@NumeroSerie", Activo.NumeroSerie);

                Parametros.Add("@ModeloProcesador", Activo.ModeloProcesador);
                Parametros.Add("@FrecuenciaProcesador", Activo.FrecuenciaProcesador);
                Parametros.Add("@NucleosProcesador", Activo.NucleosProcesador);
                Parametros.Add("@MemoriaRam", Activo.MemoriaRam);
                Parametros.Add("@MemoriaVideo", Activo.MemoriaVideo);
                Parametros.Add("@AceleradoraGrafica", Activo.AceleradoraGrafica);
                Parametros.Add("@TamañoDisco", Activo.TamañoDisco);


                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public int CambiarEstado(ActivoBE Activo)

        {
            try
            {
                string Consulta = "ActivoCambiarEstado";
                Hashtable Parametros = new Hashtable();

                string Estado = Activo.Estado.GetType().ToString();
                Estado = Estado.Substring(0, Estado.Length - 2);
                Estado = Estado.Substring(3);

                Parametros.Add("@Id", Activo.Id);
                Parametros.Add("@NuevoEstado", Estado);
                Parametros.Add("@UsuarioModificacion", Activo.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", Activo.FechaModificacion);



                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }
        public int Eliminar(ActivoBE Activo)

        {
            try
            {
                string Consulta = "ActivoEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Activo.Id);
                Parametros.Add("@UsuarioModificacion", Activo.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", Activo.FechaModificacion);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }


        public ActivoTipoBE ObtenerTipoPorId(ActivoTipoBE tipo)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", tipo.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("ActivoTipoObtenerPorId", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    tipo.Descripcion = Item["Descripcion"].ToString().Trim();
                    tipo.ArquitecturaPc = Convert.ToBoolean(Item["ArquitecturaPc"]);


                }
            }

            return tipo;

        }



        public List<ActivoTipoBE> ListarTipos()

        {
            try
            {
                List<ActivoTipoBE> Lista = new List<ActivoTipoBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("ActivoTipoListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        ActivoTipoBE tipo = new ActivoTipoBE();

                        tipo.Id = Convert.ToInt32(Item["Id"]);
                        tipo.Descripcion = Item["Descripcion"].ToString().Trim();
                        tipo.ArquitecturaPc = Convert.ToBoolean(Item["ArquitecturaPc"]);
                        tipo.Cantidad = Convert.ToInt32(Item["Cantidad"]);

                        Lista.Add(tipo);
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

        public ActivoTipoBE ObtenerEstadoPorId(ActivoTipoBE tipo)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", tipo.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("ActivoEstadoObtenerPorId", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    tipo.Descripcion = Item["Descripcion"].ToString().Trim();

                }
            }

            return tipo;

        }
    }
}
