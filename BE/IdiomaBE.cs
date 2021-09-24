using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class IdiomaBE : EntityBE
    {
        public IdiomaBE() { }

        public IdiomaBE(string _descripcion)
        {

        }

        private string descripcion;
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [DisplayName("Descripción")]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private bool porDefecto;

        public bool PorDefecto

        {
            get { return porDefecto; }
            set { porDefecto = value; }
        }

        private int porcentajeTraducido;

        public int PorcentajeTraducido 
            
        {
            get { return porcentajeTraducido; }
            set { porcentajeTraducido = value; }
        }


    }
}
