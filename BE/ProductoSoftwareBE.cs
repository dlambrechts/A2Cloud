using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductoSoftwareBE:EntityBE
    {
        private string descripcion;
        public string Descripcion { get => descripcion; set => descripcion = value; }

        private MarcaBE marca;
        public MarcaBE Marca { get => marca; set => marca = value; }

        public ProductoSoftwareBE() 
        
        {
            marca = new MarcaBE();
        }

    }
}
