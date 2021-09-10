using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using System.Collections;

namespace MPP
{
    public class BitacoraMPP
    {
        Acceso AccesoDB = new Acceso();
        public int Registrar(BitacoraBE registro)
        
        {            
                string Consulta = "BitacoraInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Fecha", registro.FechaCreacion);
                Parametros.Add("@Detalle", registro.Detalle);
                Parametros.Add("@Usuario", registro.UsuarioCreacion.Id);


            return AccesoDB.Escribir(Consulta, Parametros);
         }

        public List<BitacoraBE> ListarTodos()

        {
            List<BitacoraBE> ListaRegistros = new List<BitacoraBE>();

            Acceso AccesoDB = new Acceso();
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("BitacoraListar", null);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    CredencialBE cred = new CredencialBE();
                    UsuarioBE us = new UsuarioBE();
                    us.Credencial = cred;
                    BitacoraBE registro = new BitacoraBE(us);

                    registro.Id = Convert.ToInt32(Item[0]);
                    registro.FechaCreacion = Convert.ToDateTime(Item["Fecha"]);
                    registro.Detalle = Item["Detalle"].ToString().Trim();
                    registro.UsuarioCreacion.Id= Convert.ToInt32(Item["Usuario"]);
                    registro.UsuarioCreacion = us;
                    registro.UsuarioCreacion.Credencial.Mail = Item["Mail"].ToString().Trim();

                    ListaRegistros.Add(registro);
                }

                return ListaRegistros;
            }
            else
            {
                return null;
            }
        }

    }
}
