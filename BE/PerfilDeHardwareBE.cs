using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PerfilDeHardwareBE:EntityBE
    {
        private string descripcion;
        private int memoriaRamMinima;
        private int almecenamientoMinimo;
        private int nucleosProcesadorMinimo;
        private int memoriaVideoMinima;
        private decimal frecuenciaProcesadorMinima;
        private bool requiereAceleradoraGrafica;
        private int cantidadMonitores;

        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int MemoriaRamMinima { get => memoriaRamMinima; set => memoriaRamMinima = value; }
        public int AlmecenamientoMinimo { get => almecenamientoMinimo; set => almecenamientoMinimo = value; }
        public int NucleosProcesadorMinimo { get => nucleosProcesadorMinimo; set => nucleosProcesadorMinimo = value; }
        public int MemoriaVideoMinima { get => memoriaVideoMinima; set => memoriaVideoMinima = value; }
        public decimal FrecuenciaProcesadorMinima { get => frecuenciaProcesadorMinima; set => frecuenciaProcesadorMinima = value; }
        public bool RequiereAceleradoraGrafica { get => requiereAceleradoraGrafica; set => requiereAceleradoraGrafica = value; }
        public int CantidadMonitores { get => cantidadMonitores; set => cantidadMonitores = value; }
    }
}
