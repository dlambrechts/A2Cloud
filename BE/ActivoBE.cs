using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class ActivoBE : EntityBE
    {
        private string nombre;

        [Required]
        public string Nombre 
        
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private DateTime fechaCompra;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra

        {
            get { return fechaCompra; }
            set { fechaCompra = value; }
        }

        private ActivoTipoBE tipo;

        public ActivoTipoBE Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private MarcaBE marca;

        public MarcaBE Marca

        {
            get { return marca; }
            set { marca = value; }
        }

        private string modelo;

        public string Modelo

        {
            get { return modelo; }
            set { modelo = value; }
        }

        private string numeroSerie;

        [Required]
        public string NumeroSerie

        {
            get { return numeroSerie; }
            set { numeroSerie = value; }
        }


        private int cicloDeVida;

        [Range(1, 10)]
        public int CicloDeVida 
        
        { 
            get { return cicloDeVida; }
            set { cicloDeVida = value; }
        }

        private DateTime finCicloDeVida;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FinCicloDeVida { get => FechaCompra.AddYears(CicloDeVida); set => finCicloDeVida = value; }



        private int memoriaRam;
        [Range(0, 9999)]
        public int MemoriaRam { get => memoriaRam; set => memoriaRam = value; }
               
        private int tamañoDisco;

        [Range(0, 9999, ErrorMessage = "Ingrese un valor del 0 al 9999")]
        public int TamañoDisco { get => tamañoDisco; set => tamañoDisco = value; }

        
        private string modeloProcesador;

        [Required(ErrorMessage = "El modelo es obligatorio")]
        public string ModeloProcesador { get => modeloProcesador; set => modeloProcesador = value; }

        private decimal frecuenciaProcesador;
        public decimal FrecuenciaProcesador { get => frecuenciaProcesador; set => frecuenciaProcesador = value; }

        
        private int nucleosProcesador;
        [Range(0, 99, ErrorMessage = "Ingrese un valor del 0 al 99")]
        public int NucleosProcesador { get => nucleosProcesador; set => nucleosProcesador = value; }

        private float memoriaVideo;
        [Range(0, 99, ErrorMessage = "Ingrese un valor del 0 al 99")]
        public float MemoriaVideo { get => memoriaVideo; set => memoriaVideo = value; }

        private bool aceleradoraGrafica;
        public bool AceleradoraGrafica { get => aceleradoraGrafica; set => aceleradoraGrafica = value; }

        public string DescripcionLarga { get => Nombre +" (" + tipo.Descripcion + " S/N: " +numeroSerie+" )" ; }

        public string DescSerie { get => Nombre + " " + NumeroSerie; }

        private ActivoEstadoBE estado;

        public ActivoEstadoBE Estado { get { return estado; } }

        public void CambiarEstado(ActivoEstadoBE est)
        {

            this.estado = est;

        }

        public override string ToString()
        {
            return Nombre;
        }

        public ActivoBE()

        {
            marca = new MarcaBE();
            tipo = new ActivoTipoBE();
            estado = new ActivoEstadoDisponibleBE();
        }

    }
}
