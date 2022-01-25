using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using GestorDeArchivo;

namespace DAL
{
    public class Acceso
    {
        private SqlConnection Conexion;
        private SqlTransaction Transaccion;
        private SqlCommand ComandoSQL;
        private string CadenaConexion = "Data Source=.;Initial Catalog=a2cloud;User=sa;Password=Remiser0";
        //private string CadenaConexion = "Data Source=EQA-NB09\\SQLEXPRESS;Initial Catalog=A2Cloud;User ID=sa;Password=********";




        private SqlConnection AbrirConexion()

        {
            Conexion = new SqlConnection(CadenaConexion);
            Conexion.Open();
            return Conexion;
        }

        public DataSet LeerDatos(string Consuta, Hashtable Parametros)

        {
            DataSet DS = new DataSet();
            ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = AbrirConexion();
            ComandoSQL.CommandText = Consuta;
            ComandoSQL.CommandType = CommandType.StoredProcedure;

            if ((Parametros != null))
            {

                foreach (string dato in Parametros.Keys)
                {
                    ComandoSQL.Parameters.AddWithValue(dato, Parametros[dato]);
                }
            }

            SqlDataAdapter Adaptador = new SqlDataAdapter(ComandoSQL);
            Adaptador.Fill(DS);
            Conexion.Close();
            return DS;
        }


        public int Escribir(string Consulta, Hashtable Parametros)
        {
            ComandoSQL = new SqlCommand();
            ComandoSQL.Connection = AbrirConexion();

            int Afectadas;

            try
            {
                Transaccion = Conexion.BeginTransaction();
                ComandoSQL.CommandText = Consulta;
                ComandoSQL.CommandType = CommandType.StoredProcedure;
                ComandoSQL.Transaction = Transaccion;

                if ((Parametros != null))
                {
                    foreach (string dato in Parametros.Keys)
                    {
                        ComandoSQL.Parameters.AddWithValue(dato, Parametros[dato]);
                    }
                }
               
                Afectadas = ComandoSQL.ExecuteNonQuery();
                Transaccion.Commit();          
            }

            catch (Exception ex)
            {               
                Transaccion.Rollback();
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }

            finally
            {
                Conexion.Close();
            }
            return Afectadas;
        }

        public void QueryBackup(string Consulta) // Exclusivo para tareas de backup: se conecta a la base MASTER y no utiliza Transacciones
        {
            ComandoSQL = new SqlCommand();
            Conexion = new SqlConnection("Data Source=.;Initial Catalog=master;User=sa;Password=******);
            Conexion.Open();
            ComandoSQL.Connection = Conexion;

            try
            {
                ComandoSQL.CommandText = Consulta;
                ComandoSQL.CommandTimeout = 600;
                ComandoSQL.CommandType = CommandType.Text;
                ComandoSQL.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                FileMananager.RegistrarError(ex.Message);
            }
            finally
            {
                Conexion.Close();
            }
        }

    }
}
