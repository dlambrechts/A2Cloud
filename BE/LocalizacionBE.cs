using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class LocalizacionBE:EntityBE
    {

        private string descripcion;
        private UbicacionBE ubicacion;
        [Required(ErrorMessage = "La Descripción es Obligatoria")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public UbicacionBE Ubicacion { get => ubicacion; set => ubicacion = value; }

        public LocalizacionBE() 
        {
            ubicacion = new UbicacionBE();
        }
    }
}
