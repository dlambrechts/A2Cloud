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
    }
}
