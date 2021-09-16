using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using BE;
using System.Collections;

namespace MPP
{
    public class IdiomaMPP
    {
        Acceso AccesoDB = new Acceso();

        public List<IdiomaBE> ObtenerIdiomas()

        {
            List<IdiomaBE> ListaIdiomas = new List<IdiomaBE>();

            Acceso AccesoDB = new Acceso();
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("IdiomaListar", null);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    IdiomaBE oIdioma = new IdiomaBE();
                    UsuarioBE userCre = new UsuarioBE();
                    UsuarioBE userMod = new UsuarioBE();
                    oIdioma.UsuarioCreacion = userCre;
                    oIdioma.UsuarioModificacion = userMod;
                    oIdioma.Id = Convert.ToInt32(Item["Id"]);
                    oIdioma.Descripcion = Item["Descripcion"].ToString().Trim();                   
                    oIdioma.UsuarioCreacion.Id = Convert.ToInt32(Item["UsuarioCreacion"]);
                    if ((Item["FechaCreacion"]) != DBNull.Value) { oIdioma.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }                  
                    if ((Item["FechaModificacion"]) != DBNull.Value) { oIdioma.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]); }
                    if ((Item["UsuarioModificacion"]) != DBNull.Value) {oIdioma.UsuarioModificacion.Id = Convert.ToInt32(Item["UsuarioModificacion"]); }
                    oIdioma.PorDefecto = Convert.ToBoolean(Item["PorDefecto"]);

                    ListaIdiomas.Add(oIdioma);
                }

                return ListaIdiomas;
            }
            else
            {
                return null;
            }

        }

        public int Insertar(IdiomaBE Idioma)

        {
            string Consulta = "IdiomaInsertar";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Descripcion", Idioma.Descripcion);
            Parametros.Add("@FechaCreacion", Idioma.FechaCreacion);
            Parametros.Add("@UsuarioCreacion", Idioma.UsuarioCreacion.Id);
            Parametros.Add("@PorDefecto", Idioma.PorDefecto);

            return AccesoDB.Escribir(Consulta, Parametros);
        }


        public IdiomaBE ObtenerUno(IdiomaBE idioma)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", idioma.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("IdiomaObtenerUno", Param);

            IdiomaBE oIdioma = new IdiomaBE();

            if (DS.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow Item in DS.Tables[0].Rows)
                {


                    UsuarioBE userCre = new UsuarioBE();
                    UsuarioBE userMod = new UsuarioBE();
                    oIdioma.UsuarioCreacion = userCre;
                    oIdioma.UsuarioModificacion = userMod;
                    oIdioma.Id = Convert.ToInt32(Item["Id"]);
                    oIdioma.Descripcion = Item["Descripcion"].ToString().Trim();
                    oIdioma.UsuarioCreacion.Id = Convert.ToInt32(Item["UsuarioCreacion"]);
                    if ((Item["FechaCreacion"]) != DBNull.Value) { oIdioma.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) { oIdioma.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]); }
                    if ((Item["UsuarioModificacion"]) != DBNull.Value) { oIdioma.UsuarioModificacion.Id = Convert.ToInt32(Item["UsuarioModificacion"]); }
                    oIdioma.PorDefecto = Convert.ToBoolean(Item["PorDefecto"]);


                }

            }
            return oIdioma;
        }

        public void Editar(IdiomaBE idioma)

        {
            string Consulta = "IdiomaEditar";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Id", idioma.Id);
            Parametros.Add("@Descripcion", idioma.Descripcion);
            Parametros.Add("@FechaModificacion", idioma.FechaModificacion);
            Parametros.Add("@UsuarioModificacion", idioma.UsuarioModificacion.Id);
            Parametros.Add("@PorDefecto", idioma.PorDefecto);


            Acceso nAcceso = new Acceso();

            nAcceso.Escribir(Consulta, Parametros);
        }

        public void Eliminar(IdiomaBE Idioma)

        {
            string Consulta = "IdiomaEliminar";
            Hashtable Parametros = new Hashtable();

            Parametros.Add("@Id", Idioma.Id);
            Parametros.Add("@FechaModificacion", Idioma.FechaModificacion);
            Acceso nAcceso = new Acceso();

            nAcceso.Escribir(Consulta, Parametros);

        }

        public Dictionary<string, IdiomaTraduccionBE> ObtenerTraduccionesDic(IdiomaBE Idioma)

        {
            Dictionary<string, IdiomaTraduccionBE> ListaTraducciones = new Dictionary<string, IdiomaTraduccionBE>();

            DataSet DS = new DataSet();
            Hashtable Param = new Hashtable();
            Param.Add("@Idioma", Idioma.Id);
            DS = AccesoDB.LeerDatos("IdiomaObtenerTraduccionesDic", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    IdiomaTraduccionBE Traduccion = new IdiomaTraduccionBE();
                    IdiomaEtiquetaBE Etiqueta = new IdiomaEtiquetaBE();

                    Etiqueta.Id = Convert.ToInt32(Item[0]);
                    Etiqueta.Nombre = Item[1].ToString().Trim();
                    Traduccion.Etiqueta = Etiqueta;
                    Traduccion.Texto = Item[2].ToString().Trim();

                    ListaTraducciones.Add(Etiqueta.Nombre, Traduccion);
                }

                return ListaTraducciones;
            }
            else
            {
                return null;
            }
        }

        public List<IdiomaTraduccionBE> ObtenerTraducciones(IdiomaBE Idioma)

        {
            List<IdiomaTraduccionBE> ListaTraducciones = new List<IdiomaTraduccionBE>();

            DataSet DS = new DataSet();
            Hashtable Param = new Hashtable();
            Param.Add("@Idioma", Idioma.Id);
            DS = AccesoDB.LeerDatos("IdiomaObtenerTraducciones", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    IdiomaTraduccionBE Traduccion = new IdiomaTraduccionBE();

                    Traduccion.Etiqueta.Id = Convert.ToInt32(Item[0]);
                    Traduccion.Etiqueta.Nombre = Item[1].ToString().Trim();                   
                    Traduccion.Texto = Item[2].ToString().Trim();

                    ListaTraducciones.Add(Traduccion);
                }

                return ListaTraducciones;
            }
            else
            {
                return null;
            }
        }

        public List<IdiomaEtiquetaBE> ObtenerEtiquetas()

        {
            List<IdiomaEtiquetaBE> ListaEtiquetas = new List<IdiomaEtiquetaBE>();

            Acceso AccesoDB = new Acceso();
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("IdiomaObtenerEtiquetas", null);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    IdiomaEtiquetaBE oEtiqueta = new IdiomaEtiquetaBE();
                    oEtiqueta.Id = Convert.ToInt32(Item[0]);
                    oEtiqueta.Nombre = Item[1].ToString().Trim();


                    ListaEtiquetas.Add(oEtiqueta);
                }

                return ListaEtiquetas;
            }
            else
            {
                return null;
            }

        }

        public void GuardarTraduccion(IdiomaTraduccionBE traduccion) 
        
        {

                string Consulta = "IdiomaTraduccionGuardar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Idioma", traduccion.Idioma.Id);
                Parametros.Add("@Etiqueta", traduccion.Etiqueta.Id);
                Parametros.Add("@Traduccion", traduccion.Texto);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);
            }


    }
}
