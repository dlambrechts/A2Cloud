using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using GestorDeArchivo;
using System.Collections;
using System.Data;

namespace MPP
{
    public class DepartamentoMPP
    {
        Acceso AccesoDB = new Acceso();

        public int Insertar(DepartamentoBE Departamento)

        {
            try
            {
                string Consulta = "DepartamentoInsertar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Descripcion", Departamento.Descripcion);
                Parametros.Add("@FechaCreacion", Departamento.FechaCreacion);
                Parametros.Add("@UsuarioCreacion", Departamento.UsuarioCreacion.Id);


                return AccesoDB.Escribir(Consulta, Parametros);

            }
            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
                return -1;
            }
        }

        public void Editar(DepartamentoBE Departamento)

        {
            try
            {
                string Consulta = "DepartamentoEditar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Departamento.Id);
                Parametros.Add("@Descripcion", Departamento.Descripcion);
                Parametros.Add("@FechaModificacion", Departamento.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Departamento.UsuarioModificacion.Id);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }

        public List<DepartamentoBE> Listar()

        {
            try
            {
                List<DepartamentoBE> Lista = new List<DepartamentoBE>();

                Acceso AccesoDB = new Acceso();
                DataSet DS = new DataSet();
                DS = AccesoDB.LeerDatos("DepartamentoListar", null);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {

                        DepartamentoBE Departamento = new DepartamentoBE();

                        Departamento.Id = Convert.ToInt32(Item["Id"]);
                        Departamento.Descripcion = Item["Descripcion"].ToString().Trim();
                        if ((Item["FechaCreacion"]) != DBNull.Value) { Departamento.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) Departamento.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                        Lista.Add(Departamento);
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

        public DepartamentoBE ObtenerUno(DepartamentoBE Departamento)

        {
            Hashtable Param = new Hashtable();
            Param.Add("@Id", Departamento.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("DepartamentoObtenerPorId", Param);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    Departamento.Descripcion = Item["Descripcion"].ToString().Trim();
                    if ((Item["FechaCreacion"]) != DBNull.Value) { Departamento.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) Departamento.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]);

                }
            }

            return Departamento;

        }

        public void Eliminar(DepartamentoBE Departamento)

        {
            try
            {
                string Consulta = "DepartamentoEliminar";
                Hashtable Parametros = new Hashtable();

                Parametros.Add("@Id", Departamento.Id);
                Parametros.Add("@FechaModificacion", Departamento.FechaModificacion);
                Parametros.Add("@UsuarioModificacion", Departamento.UsuarioModificacion.Id);


                Acceso nAcceso = new Acceso();

                nAcceso.Escribir(Consulta, Parametros);

            }

            catch (Exception ex)

            {
                FileMananager.RegistrarError(ex.Message);
            }
        }
    }
}
