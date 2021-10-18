using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class MarcaBE:EntityBE
    {
        private string descripcion;

        [Required(ErrorMessage = "La Descripción es Obligatoria")]
        public string Descripcion 
        
        { 
            get { return descripcion; }
            set { descripcion = value; }
        }
          
    }
}
