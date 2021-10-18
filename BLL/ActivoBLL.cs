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
    }
}
