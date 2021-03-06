using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class ActivoBLL
    {
        ActivoMPP mppActivo = new ActivoMPP();


        public void Insertar (ActivoBE Activo) 
        
        {
            Activo.FechaCreacion = DateTime.Now;

            Activo.CambiarEstado(new ActivoEstadoDisponibleBE());

            if (Activo.Tipo.ArquitecturaPc == true) { 

            mppActivo.InsertarPc(Activo);
            }

            else mppActivo.Insertar(Activo);

        }

        public void Editar (ActivoBE Activo) 
        
        {

            Activo.FechaModificacion = DateTime.Now;

            if (Activo.Tipo.ArquitecturaPc == true) 
            
            {
                mppActivo.EditarPc(Activo);
            }

            else { mppActivo.Editar(Activo); }
        }

        public int CambiarEstado(ActivoBE Activo) 
        
        {
            Activo.FechaModificacion = DateTime.Now;
            return mppActivo.CambiarEstado(Activo);
        }

        public ActivoBE ObtenerPorId (ActivoBE Activo) 
        
        {
           return mppActivo.ObtenerPorId(Activo);
        }

        public List<ActivoTipoBE> ListarTipos() 
        
        {
            return mppActivo.ListarTipos();        
        }

        public ActivoTipoBE ObtenerTipoPorId (ActivoTipoBE tipo) 
        
        {
            return mppActivo.ObtenerTipoPorId(tipo);
        }

        public List<ActivoBE> Listar() 
        
        {
            return mppActivo.Listar();
        }

        public int Eliminar(ActivoBE Activo)        
        {
            Activo.FechaModificacion = DateTime.Now;

            return mppActivo.Eliminar(Activo);
        }

        public List<ActivoEstadoBE> Estados()

        {
            List<ActivoEstadoBE> Estados = new List<ActivoEstadoBE>();

            ActivoEstadoDisponibleBE Disponible = new ActivoEstadoDisponibleBE();
            Disponible.Codigo = "BE.ActivoEstadoDisponibleBE"; Disponible.Descripcion = "Disponible";
            Estados.Add(Disponible);

            ActivoEstadoAsignadoBE Asignado = new ActivoEstadoAsignadoBE();
            Asignado.Codigo = "BE.ActivoEstadoAsignadoBE"; Asignado.Descripcion = "Asignado";
            Estados.Add(Asignado);

            ActivoEstadoBajaBE Baja = new ActivoEstadoBajaBE();
            Baja.Codigo = "BE.ActivoEstadoBajaBE"; Baja.Descripcion = "Baja";
            Estados.Add(Baja);

            return Estados;


        }

    }
}
