using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class ProductoSoftwareBLL
    {
        ProductoSoftwareMPP mppSoft = new ProductoSoftwareMPP();

        public List<ProductoSoftwareBE> Listar() 
        {
            return mppSoft.Listar();
        }

        public ProductoSoftwareBE ObtenerUno(ProductoSoftwareBE Soft)
        {

            return mppSoft.ObtenerUno(Soft);
        }

        public void Insertar (ProductoSoftwareBE Soft) 
        
        {
            Soft.FechaCreacion = DateTime.Now;

            mppSoft.Insertar(Soft);
        
        }

        public void Editar ( ProductoSoftwareBE Soft) 
        {
            Soft.FechaModificacion = DateTime.Now;

            mppSoft.Editar(Soft);
        }

        public void Eliminar (ProductoSoftwareBE Soft) 
        
        {
            Soft.FechaModificacion = DateTime.Now;

            mppSoft.Eliminar(Soft);
        }

    }
}
