using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class ColaboradorBE:EntityBE
    {
        private string nombre;
        private string apellido;
        private string mail;
        private DepartamentoBE departamento;
        private PerfilDeHardwareBE perfilHardware;
        private UbicacionBE ubicacion;
        private LocalizacionBE localizacion;
        private bool fullRemoto;

        [Required]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Apellido { get => apellido; set => apellido = value; }
        [EmailAddress]
        [Required]
        public string Mail { get => mail; set => mail = value; }
        public DepartamentoBE Departamento { get => departamento; set => departamento = value; }
        public PerfilDeHardwareBE PerfilHardware { get => perfilHardware; set => perfilHardware = value; }
        public UbicacionBE Ubicacion { get => ubicacion; set => ubicacion = value; }
        public bool FullRemoto { get => fullRemoto; set => fullRemoto = value; }
        public LocalizacionBE Localizacion { get => localizacion; set => localizacion = value; }



        public override string ToString()
        {
            return Nombre+" "+Apellido;
        }

        public string NombreCompleto { get { return nombre + " " + apellido; } }

        public ColaboradorBE() 
        
        {
            departamento = new DepartamentoBE();
            perfilHardware = new PerfilDeHardwareBE();
            ubicacion = new UbicacionBE();
            localizacion = new LocalizacionBE();
        
        }
    }
}
