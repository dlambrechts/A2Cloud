using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using DAL;
using BE;
using GestorDeArchivo;

namespace MPP
{
    public class PerfilMPP
    {
        Acceso AccesoDB = new Acceso();
        public List<PerfilPatenteBE> ObtenerPatentes()
        {
            List<PerfilPatenteBE> ListaPatentes = new List<PerfilPatenteBE>();


            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilPatentesListar", null);

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    PerfilPatenteBE oPatente = new PerfilPatenteBE();

                    oPatente.Id = Convert.ToInt32(Item["Id"]);
                    oPatente.Permiso =  Convert.ToString(Item["Permiso"]).Trim();
                    oPatente.Descripcion = Item["Descripcion"].ToString().Trim();

                    ListaPatentes.Add(oPatente);
                }

                return ListaPatentes;
            }
            else
            {
                return null;
            }
        }


        public PerfilFamiliaBE ObtenerFamiliaPorId(PerfilFamiliaBE oFamilia)

        {

            Hashtable Param = new Hashtable();
            Param.Add("@Id", oFamilia.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilFamiliaObtenerPorId", Param);


            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {
     
                    oFamilia.Descripcion = Item["Descripcion"].ToString().Trim();
                    oFamilia.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]);
                    oFamilia.UsuarioCreacion = new UsuarioBE();
                    if ((Item["UsuarioCreacion"]) != DBNull.Value) { oFamilia.UsuarioCreacion.Id = Convert.ToInt32(Item["UsuarioCreacion"]); }
                    if ((Item["FechaCreacion"]) != DBNull.Value) { oFamilia.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }

                    oFamilia.UsuarioModificacion = new UsuarioBE();
                    if ((Item["UsuarioModificacion"]) != DBNull.Value) { oFamilia.UsuarioModificacion.Id = Convert.ToInt32(Item["UsuarioModificacion"]); }
                    if ((Item["FechaModificacion"]) != DBNull.Value) { oFamilia.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]); }
                }
            }

            return oFamilia;


        }
        public IList<PerfilFamiliaBE> ObtenerFamilias()
        {
            List<PerfilFamiliaBE> ListaFamilias = new List<PerfilFamiliaBE>();

            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilFamiliaListar", null);

            try
            {

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Item in DS.Tables[0].Rows)
                    {
                        PerfilFamiliaBE oFamilia = new PerfilFamiliaBE();

                        oFamilia.Id = Convert.ToInt32(Item["Id"]);
                        oFamilia.Descripcion = Item["Descripcion"].ToString().Trim();
                        oFamilia.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]);
                        oFamilia.UsuarioCreacion = new UsuarioBE();
                        if ((Item["UsuarioCreacion"]) != DBNull.Value) { oFamilia.UsuarioCreacion.Id = Convert.ToInt32(Item["UsuarioCreacion"]); }
                        if ((Item["FechaCreacion"]) != DBNull.Value) { oFamilia.FechaCreacion = Convert.ToDateTime(Item["FechaCreacion"]); }

                        oFamilia.UsuarioModificacion = new UsuarioBE();
                        if ((Item["UsuarioModificacion"]) != DBNull.Value) { oFamilia.UsuarioModificacion.Id = Convert.ToInt32(Item["UsuarioModificacion"]); }
                        if ((Item["FechaModificacion"]) != DBNull.Value) { oFamilia.FechaModificacion = Convert.ToDateTime(Item["FechaModificacion"]); }


                        ListaFamilias.Add(oFamilia);
                    }

                    return ListaFamilias;
                }
                else
                {
                    return ListaFamilias;
                }

            } catch (Exception ex) { FileMananager.RegistrarError(ex.Message); return null; }
        }

        public void GuardarFamilia(PerfilFamiliaBE Fam)

        {

            string ConsultaDel = "PerfilFamiliaEliminarComponentes"; // Primero borro los componentes
            Hashtable ParametrosDel = new Hashtable();
            ParametrosDel.Add("Id", Fam.Id);
            AccesoDB.Escribir(ConsultaDel, ParametrosDel);

            string ConsultaAdd = "PerfilFamiliaGuardar"; // Luego guardo la familia actualizada
            Hashtable ParametrosAdd = new Hashtable();
            ParametrosAdd.Add("IdPadre", Fam.Id);
            ParametrosAdd.Add("FechaModificacion", Fam.FechaModificacion);
            ParametrosAdd.Add("UsuarioModificacion", Fam.UsuarioModificacion.Id);

            foreach (var item in Fam.Hijos)
            {

                ParametrosAdd.Add("IdHijo", item.Id);
                AccesoDB.Escribir(ConsultaAdd, ParametrosAdd);
                ParametrosAdd.Remove("IdHijo");
            }
        }



        public IList<PerfilComponenteBE> ObtenerTodo(PerfilFamiliaBE Familia)
        {

            Hashtable Parametros = new Hashtable();
            Parametros.Add("Fam", Familia.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilFamiliaCompleta", Parametros);

            var Lista = new List<PerfilComponenteBE>();

            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Item in DS.Tables[0].Rows)
                {

                    int id_padre = 0;
                    if (Item["IdPadre"] != DBNull.Value)
                    {
                        id_padre = Convert.ToInt32(Item["IdPadre"]);
                    }

                    var id = Convert.ToInt32(Item["Id"]);
                    var nombre = Convert.ToString(Item["Descripcion"]).Trim();

                    var permiso = string.Empty;
                    if (Item["Permiso"] != DBNull.Value)
                        permiso = Convert.ToString(Item["Permiso"]).Trim();


                    PerfilComponenteBE c;

                    if (string.IsNullOrEmpty(permiso))
                        c = new PerfilFamiliaBE();

                    else
                        c = new PerfilPatenteBE();

                    c.Id = id;
                    c.Descripcion = nombre;
                    if (!string.IsNullOrEmpty(permiso))
                        c.Permiso = permiso;

                    var padre = ObtenerComponente(id_padre, Lista);

                    if (padre == null)
                    {
                        Lista.Add(c);
                    }
                    else
                    {
                        padre.AgregarHijo(c);
                    }

                }
            }

            return Lista;
        }

        public void CompletarComponentesFamilia(PerfilFamiliaBE familia)
        {
            familia.VaciarHijos();
            foreach (var item in ObtenerTodo(familia))
            {
                familia.AgregarHijo(item);
            }
        }

        private PerfilComponenteBE ObtenerComponente(int id, IList<PerfilComponenteBE> lista)
        {

            PerfilComponenteBE Componente = lista != null ? lista.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

            if (Componente == null && lista != null)
            {
                foreach (var c in lista)
                {

                    var l = ObtenerComponente(id, c.Hijos);
                    if (l != null && l.Id == id) return l;
                    else
                    if (l != null)
                        return ObtenerComponente(id, l.Hijos);
                }
            }

            return Componente;
        }

        public void EditarFamilia(PerfilFamiliaBE fam)

    
            {
                Hashtable Parametros = new Hashtable();

                Parametros.Add("Id", fam.Id);
                Parametros.Add("Descripcion", fam.Descripcion);
                Parametros.Add("FechaModificacion", fam.FechaModificacion);
                Parametros.Add("UsuarioModificacion", fam.UsuarioModificacion.Id);

                string Consulta = "PerfilFamiliaEditar";
                AccesoDB.Escribir(Consulta, Parametros);

            }

        public void EliminarFamilia(PerfilFamiliaBE fam)

        {
          

            string ConsultaDel = "PerfilFamiliaEliminarComponentes"; // Primero borro los componentes
            Hashtable ParametrosDel = new Hashtable();
            ParametrosDel.Add("Id", fam.Id);

            AccesoDB.Escribir(ConsultaDel, ParametrosDel);

            ParametrosDel.Add("FechaModificacion", fam.FechaModificacion);
            ParametrosDel.Add("UsuarioModificacion", fam.UsuarioModificacion.Id);

            string Consulta = "PerfilFamiliaEliminar";
            AccesoDB.Escribir(Consulta, ParametrosDel);

        }

        public void CrearFamilia (PerfilFamiliaBE fam)
        {
            Hashtable Parametros = new Hashtable();

            Parametros.Add("Descripcion", fam.Descripcion);
            Parametros.Add("FechaCreacion", fam.FechaCreacion);
            Parametros.Add("UsuarioCreacion", fam.UsuarioCreacion.Id);

            string Consulta = "PerfilFamiliaInsertar";
            AccesoDB.Escribir(Consulta, Parametros);

        }
        public bool VerificarPermisoImplicito(PerfilFamiliaBE padre, PerfilComponenteBE hijo) 
        
        {
            Hashtable Parametros = new Hashtable();

            Parametros.Add("fam", hijo.Id);
    
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilFamiliaCompleta", Parametros);

            bool resultado = false;

                if (DS.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow Item in DS.Tables[0].Rows)
                
                    {
                    if (Convert.ToInt32(Item["IdHijo"]) == padre.Id){ resultado= true; }
                                         
                    }


                }
                else
                {
                return resultado;
                }

            return resultado;
        }

        public bool FamiliaVerificarUso(PerfilFamiliaBE fam) 
        
        {
            Hashtable Parametros = new Hashtable();

            Parametros.Add("Id", fam.Id);

            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PerfilFamiliaVerificarUso", Parametros);

            bool resultado = false;

            if (DS.Tables[0].Rows.Count > 0)
            {

                resultado = true;

            }
            else
            {
                return resultado;
            }

            return resultado;
        }

        public void CargarPerfilUsuario(UsuarioBE Us)

        {
         
            Hashtable Parametros = new Hashtable();
            Parametros.Add("IdUsuario", Us.Id);
            DataSet DS = new DataSet();
            DS = AccesoDB.LeerDatos("PermisosUsuarioListar", Parametros);

            Us.Permisos.Clear();
            if (DS.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow Item in DS.Tables[0].Rows)
                {
                    var IdPermiso = Convert.ToInt32(Item["Id"]);
                    var DescPermiso = Convert.ToString(Item["Descripcion"]).Trim();
                    var Permiso = string.Empty;

                    if (Item["Permiso"] != DBNull.Value) { Permiso = Convert.ToString(Item["Permiso"]).Trim(); }



                    if (!String.IsNullOrEmpty(Permiso))

                    {
                        PerfilPatenteBE Patente = new PerfilPatenteBE();
                        Patente.Id = IdPermiso;
                        Patente.Descripcion = DescPermiso;
                        Patente.Permiso = Permiso;
                        Us.Permisos.Add(Patente);
                    }

                    else

                    {
                        PerfilFamiliaBE Familia = new PerfilFamiliaBE();                     
                        Familia.Id = IdPermiso;
                        Familia.Descripcion = DescPermiso;

                        var Arbol = ObtenerTodo(Familia);

                        foreach (var hijo in Arbol)

                        {
                            Familia.AgregarHijo(hijo);
                        }

                        Us.Permisos.Add(Familia);
                    }
                }
            }

        }

    }
}
