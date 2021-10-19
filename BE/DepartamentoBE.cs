using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class DepartamentoBE:EntityBE
    {
        
        private string descripcion;

        [Required]
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
