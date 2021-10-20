using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class UbicacionBE:EntityBE
    {
        private string calle;
        private string altura;
        private string codigoPostal;
        private string piso;
        private string departamento;

        [Required]
        public string Calle { get => calle; set => calle = value; }
        [Required]
        public string Altura { get => altura; set => altura = value; }
        [Required]
        public string CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
        public string Piso { get => piso; set => piso = value; }
        public string Departamento { get => departamento; set => departamento = value; }
    }
}
