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
    public class PerfilDeHardwareMPP
    {

        Acceso AccesoDB = new Acceso();


        public int Insertar(PerfilDeHardwareBE PerfilDeHardware)

        {
            try
            {
                string Consulta = "PerfilDeHardwareInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Descripcion", PerfilDeHardware.Descripcion);
                Parametros.Add("@MemoriaRamMinima", PerfilDeHardware.MemoriaRamMinima);
                Parametros.Add("@AlmacenamientoMinimo", PerfilDeHardware.AlmecenamientoMinimo);
                Parametros.Add("@NucleosProcesadorMinimo", PerfilDeHardware.NucleosProcesadorMinimo);
                Parametros.Add("@MemoriaVideoMinima", PerfilDeHardware.MemoriaVideoMinima);
                Parametros.Add("@FrecuenciaProcesadorMinima", PerfilDeHardware.FrecuenciaProcesadorMinima);
                Parametros.Add("@RequiereAceleradoraGrafica", PerfilDeHardware.RequiereAceleradoraGrafica);
                Parametros.Add("@CantidadMonitores", PerfilDeHardware.CantidadMonitores);
                Parametros.Add("@DispositivoPrincipal", PerfilDeHardware.DispositivoPrincipal.Id);

                Parametros.Add("@UsuarioCreacion", PerfilDeHardware.UsuarioCreacion.Id);
                Parametros.Add("@FechaCreacion", PerfilDeHardware.FechaCreacion);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }
        public PerfilDeHardwareBE ObtenerUno(PerfilDeHardwareBE PerfilDeHardware)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", PerfilDeHardware.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilDeHardwareObtenerPorId", Param);

            ActivoMPP mppActivo = new ActivoMPP();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    PerfilDeHardware.Descripcion = Item["Descripcion"].ToString().Trim();
                    if ((Item["MemoriaRamMinima"]) != DBNull.Value) { PerfilDeHardware.MemoriaRamMinima = Convert.ToInt32(Item["MemoriaRamMinima"]); }
                    if ((Item["AlmacenamientoMinimo"]) != DBNull.Value) { PerfilDeHardware.AlmecenamientoMinimo = Convert.ToInt32(Item["AlmacenamientoMinimo"]); }
                    if ((Item["NucleosProcesadorMinimo"]) != DBNull.Value) { PerfilDeHardware.NucleosProcesadorMinimo = Convert.ToInt32(Item["NucleosProcesadorMinimo"]); }
                    if ((Item["MemoriaVideoMinima"]) != DBNull.Value) { PerfilDeHardware.MemoriaVideoMinima = Convert.ToInt32(Item["MemoriaVideoMinima"]); }
                    if ((Item["FrecuenciaProcesadorMinima"]) != DBNull.Value) { PerfilDeHardware.FrecuenciaProcesadorMinima = Convert.ToDecimal(Item["FrecuenciaProcesadorMinima"]); }
                    if ((Item["RequiereAceleradoraGrafica"]) != DBNull.Value) { PerfilDeHardware.RequiereAceleradoraGrafica = Convert.ToBoolean(Item["RequiereAceleradoraGrafica"]); }
                    if ((Item["CantidadMonitores"]) != DBNull.Value) { PerfilDeHardware.CantidadMonitores= Convert.ToInt32(Item["CantidadMonitores"]); }
                    PerfilDeHardware.DispositivoPrincipal.Id = Convert.ToInt32(Item["DispositivoPrincipal"]);
                    PerfilDeHardware.DispositivoPrincipal = mppActivo.ObtenerTipoPorId(PerfilDeHardware.DispositivoPrincipal);

                    if ((Item["FechaCreacion"]) != DBNull.Value) { PerfilDeHardware.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) PerfilDeHardware.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return PerfilDeHardware;

        }

        public int Editar(PerfilDeHardwareBE PerfilDeHardware)

        {
            try
            {
                string Consulta = "PerfilDeHardwareEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", PerfilDeHardware.Id);
                Parametros.Add("@Descripcion", PerfilDeHardware.Descripcion);
                Parametros.Add("@MemoriaRamMinima", PerfilDeHardware.MemoriaRamMinima);
                Parametros.Add("@AlmacenamientoMinimo", PerfilDeHardware.AlmecenamientoMinimo);
                Parametros.Add("@NucleosProcesadorMinimo", PerfilDeHardware.NucleosProcesadorMinimo);
                Parametros.Add("@MemoriaVideoMinima", PerfilDeHardware.MemoriaVideoMinima);
                Parametros.Add("@FrecuenciaProcesadorMinima", PerfilDeHardware.FrecuenciaProcesadorMinima);
                Parametros.Add("@RequiereAceleradoraGrafica", PerfilDeHardware.RequiereAceleradoraGrafica);
                Parametros.Add("@CantidadMonitores", PerfilDeHardware.CantidadMonitores);
                Parametros.Add("@DispositivoPrincipal", PerfilDeHardware.DispositivoPrincipal.Id);

                Parametros.Add("@UsuarioModificacion", PerfilDeHardware.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", PerfilDeHardware.FechaModificacion);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public int Eliminar(PerfilDeHardwareBE PerfilDeHardware)

        {
            try
            {
                string Consulta = "PerfilDeHardwareEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", PerfilDeHardware.Id);
                Parametros.Add("@UsuarioModificacion", PerfilDeHardware.UsuarioModificacion.Id);
                Parametros.Add("@FechaModificacion", PerfilDeHardware.FechaModificacion);

                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public List<PerfilDeHardwareBE> Listar()

        {
            try
            {
                List<PerfilDeHardwareBE> Lista = new List<PerfilDeHardwareBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("PerfilDeHardwareListar", null);

                ActivoMPP mppActivo = new ActivoMPP();

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        PerfilDeHardwareBE PerfilDeHardware = new PerfilDeHardwareBE();

                        PerfilDeHardware.Id = Convert.ToInt32(Item["Id"]);
                        PerfilDeHardware.Descripcion = Item["Descripcion"].ToString().Trim();
                        if ((Item["MemoriaRamMinima"]) != DBNull.Value) { PerfilDeHardware.MemoriaRamMinima = Convert.ToInt32(Item["MemoriaRamMinima"]); }
                        if ((Item["AlmacenamientoMinimo"]) != DBNull.Value) { PerfilDeHardware.AlmecenamientoMinimo = Convert.ToInt32(Item["AlmacenamientoMinimo"]); }
                        if ((Item["NucleosProcesadorMinimo"]) != DBNull.Value) { PerfilDeHardware.NucleosProcesadorMinimo = Convert.ToInt32(Item["NucleosProcesadorMinimo"]); }
                        if ((Item["MemoriaVideoMinima"]) != DBNull.Value) { PerfilDeHardware.MemoriaVideoMinima = Convert.ToInt32(Item["MemoriaVideoMinima"]); }
                        if ((Item["FrecuenciaProcesadorMinima"]) != DBNull.Value) { PerfilDeHardware.FrecuenciaProcesadorMinima = Convert.ToDecimal(Item["FrecuenciaProcesadorMinima"]); }
                        if ((Item["RequiereAceleradoraGrafica"]) != DBNull.Value) { PerfilDeHardware.RequiereAceleradoraGrafica = Convert.ToBoolean(Item["RequiereAceleradoraGrafica"]); }
                        if ((Item["CantidadMonitores"]) != DBNull.Value) { PerfilDeHardware.CantidadMonitores = Convert.ToInt32(Item["CantidadMonitores"]); }
                        PerfilDeHardware.DispositivoPrincipal.Id = Convert.ToInt32(Item["DispositivoPrincipal"]);
                        PerfilDeHardware.DispositivoPrincipal = mppActivo.ObtenerTipoPorId(PerfilDeHardware.DispositivoPrincipal);

                        if ((Item["FechaCreacion"]) != DBNull.Value) { PerfilDeHardware.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) PerfilDeHardware.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(PerfilDeHardware);
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
