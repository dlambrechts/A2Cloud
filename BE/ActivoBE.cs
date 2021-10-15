using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ActivoBE : EntityBE
    {
        private string nombre;

        public string Nombre 
        
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private DateTime fechaCompra;

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

        private int cicloDeVida;

        public int CicloDeVida 
        
        { 
            get { return cicloDeVida; }
            set { cicloDeVida = value; }
        }
        public ActivoBE()
        
        {
            marca = new MarcaBE();
            tipo = new ActivoTipoBE();
        }

    }
}
