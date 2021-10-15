using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class MarcaBLL
    {

        MarcaMPP mppMarca = new MarcaMPP();

        public void Insertar(MarcaBE Marca) 
        
        {
            Marca.FechaCreacion = DateTime.Now;

            mppMarca.Insertar(Marca);
            
        }

        public void Editar(MarcaBE Marca) 
        
        {
            Marca.FechaModificacion = DateTime.Now;

            mppMarca.Editar(Marca);
        }

        public List<MarcaBE> Listar() 
        
        {
            return mppMarca.Listar();
        }

        public MarcaBE ObtenerUno(MarcaBE Marca) 
        
        {
            return mppMarca.ObtenerUno(Marca);
        }

        public void Eliminar (MarcaBE Marca) 
        
        {
            Marca.FechaModificacion = DateTime.Now;

            mppMarca.Eliminar(Marca);

        }

    }
}
