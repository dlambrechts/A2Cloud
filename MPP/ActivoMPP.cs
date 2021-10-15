using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Collections;
using GestorDeArchivo;

namespace MPP
{
    public  class ActivoMPP
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
    }
}
