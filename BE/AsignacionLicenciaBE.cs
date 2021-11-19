using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BE
{
   public class AsignacionLicenciaBE:EntityBE
    {
        private ActivoBE activo;
        private ColaboradorBE colaborador;
        private AsignacionEstadoBE estado;
        private LicenciaBE licencia;
        private LicenciaModalidadBE modalidad;
        private int cantidadAsignada;
        private string detalle;
        private DateTime fechaInicio;
        private DateTime fechaFinalizacion;

        public ActivoBE Activo { get => activo; set => activo = value; }
        public ColaboradorBE Colaborador { get => colaborador; set => colaborador = value; }
        public AsignacionEstadoBE Estado { get => estado; set => estado = value; }
        public LicenciaBE Licencia { get => licencia; set => licencia = value; }
        public int CantidadAsignada { get => cantidadAsignada; set => cantidadAsignada = value; }

        [Required]
        public string Detalle { get => detalle; set => detalle = value; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinalizacion { get => fechaFinalizacion; set => fechaFinalizacion = value; }
        public LicenciaModalidadBE Modalidad { get => modalidad; set => modalidad = value; }

        public AsignacionLicenciaBE() 
        
        {
            activo = new ActivoBE();
            colaborador = new ColaboradorBE();
            estado = new AsignacionEstadoBE();
            licencia = new LicenciaBE();
            modalidad = new LicenciaModalidadBE();
        }
    }
}
