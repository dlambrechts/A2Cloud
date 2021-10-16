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
                    Activo.Marca=mppMarca.ObtenerUno(Activo.Marca);
                    Activo.Modelo = Item["Modelo"].ToString().Trim();
                    Activo.CicloDeVida = Convert.ToInt32(Item["CicloDeVida"]);

                    Activo.AceleradoraGrafica = Convert.ToBoolean(Item["AceleradoraGrafica"]);
                    Activo.MemoriaRam = Convert.ToInt32(Item["MemoriaRam"]);
                    Activo.TamañoDisco = Convert.ToInt32(Item["TamañoDisco"]);
                    Activo.ModeloProcesador = Item["ModeloProcesador"].ToString().Trim();
                    Activo.NucleosProcesador = Convert.ToInt32(Item["NucleosProcesador"]);
                    Activo.FrecuenciaProcesador = Convert.ToDecimal(Item["FrecuenciaProcesador"]);
                    Activo.MemoriaVideo = Convert.ToInt32(Item["MemoriaVideo"]);

                    if ((Item["FechaCreacion"]) != DBNull.Value) { Activo.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Activo.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

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

                        //if (Activo.Tipo.ArquitecturaPc == true) { 
                        //Activo.ModeloProcesador = Item["ModeloProcesador"].ToString().Trim();
                        //Activo.FrecuenciaProcesador = (float)Convert.ToDouble(Item["FrecuenciaProcesador"]);
                        //Activo.NucleosProcesador = Convert.ToInt32(Item["NucleosProcesador"]);
                        //Activo.MemoriaRam = (float)Convert.ToDouble(Item["MemoriaRam"]);
                        //Activo.MemoriaVideo = (float)Convert.ToDouble(Item["MemoriaVideo"]);
                        //Activo.TamañoDisco = Convert.ToInt32(Item["TamañoDisco"]);

                        //}

                        if ((Item["FechaCreacion"]) != DBNull.Value) { Activo.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) Activo.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

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
    }
}
