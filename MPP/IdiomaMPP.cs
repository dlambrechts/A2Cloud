using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using BE;

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
                    oIdioma.Id = Convert.ToInt32(Item[0]);
                    oIdioma.Descripcion = Item[1].ToString().Trim();


                    ListaIdiomas.Add(oIdioma);
                }

                return ListaIdiomas;
            }
            else
            {
                return null;
            }

        }

    }
}
