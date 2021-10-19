using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
   public class LicenciaBE:EntityBE
    {
        private string descripcion;
        private DateTime fechaVigencia;
        private DateTime fechaFinalizacion;
        private ProductoSoftwareBE producto;
        private int cantidad;
        private LicenciaModalidadBE modalidad;
        private string numeroContrato;

        [Required]
        public string Descripcion { get => descripcion; set => descripcion = value; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaVigencia { get => fechaVigencia; set => fechaVigencia = value; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinalizacion { get => fechaFinalizacion; set => fechaFinalizacion = value; }
        public ProductoSoftwareBE Producto { get => producto; set => producto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public LicenciaModalidadBE Modalidad { get => modalidad; set => modalidad = value; }
        public string NumeroContrato { get => numeroContrato; set => numeroContrato = value; }

        public LicenciaBE() 
        
        {
            producto = new ProductoSoftwareBE();
            modalidad = new LicenciaModalidadBE();
        }
    }
}
