using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class ProductoSoftwareBE:EntityBE
    {

        private string descripcion;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get => descripcion; set => descripcion = value; }

        private MarcaBE marca;
        public MarcaBE Marca { get => marca; set => marca = value; }

        public ProductoSoftwareBE() 
        
        {
            marca = new MarcaBE();
        }

    }
}
