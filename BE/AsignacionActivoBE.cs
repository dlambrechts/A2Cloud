using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class AsignacionActivoBE:EntityBE
    {
        private string detalle;
        private ActivoBE activo;
        private ColaboradorBE colaborador;
        private AsignacionEstadoBE estado;
        private DateTime fechaInicio;
        private DateTime fechaFinalizacion;
        private UbicacionBE ubicacion;
        private AsignacionTipoBE tipo;

        [Required]
        public string Detalle { get => detalle; set => detalle = value; }
        public ActivoBE Activo { get => activo; set => activo = value; }
        public ColaboradorBE Colaborador { get => colaborador; set => colaborador = value; }
        public AsignacionEstadoBE Estado { get => estado; set => estado = value; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFinalizacion { get => fechaFinalizacion; set => fechaFinalizacion = value; }
        public UbicacionBE Ubicacion { get => ubicacion; set => ubicacion = value; }
        public AsignacionTipoBE Tipo { get => tipo; set => tipo = value; }

        public AsignacionActivoBE() 
        
        {
            activo = new ActivoBE();
            colaborador = new ColaboradorBE();
            estado = new AsignacionEstadoBE();
            ubicacion = new UbicacionBE();
            tipo = new AsignacionTipoBE();
        }
    }
}
