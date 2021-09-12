using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using DAL;

namespace MPP
{
   public class DigitoVerificadorMPP
    {
        Acceso AccesoDB = new Acceso();
        public void ActualizarVertical(string dv)

        {
            Hashtable Parametros = new Hashtable();
            Parametros.Add("@Dvv", dv);
            Acceso AccesoDB = new Acceso();
            AccesoDB.Escribir("UsuarioActualizarDvv", Parametros);
        }

        public string ObtenerVertical()

        {
            
            Hashtable Param = new Hashtable();
            DataSet Ds = new DataSet();

            Ds = AccesoDB.LeerDatos("UsuarioObtenerDvv", null);

            string dvv="";

            if (Ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in Ds.Tables[0].Rows)
                {
                     dvv = Convert.ToString(Item["Dvv"]).Trim() ;
                }
            }
            return dvv;
        }

    }
}
