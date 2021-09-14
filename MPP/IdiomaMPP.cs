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

    }
}
