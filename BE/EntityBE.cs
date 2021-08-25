using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class EntityBE
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private UsuarioBE usuarioCreacion;
        public UsuarioBE UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set { usuarioCreacion = value; }
        }

        private DateTime fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }

        private UsuarioBE usuarioModificacion;

        public UsuarioBE UsuarioModificacion 
        { 
            get { return usuarioModificacion; }
            set { usuarioModificacion = value; }
        }
       
        private DateTime fechaModificacion;

        public DateTime FechaModificacion 
        { 
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }

    }
}
