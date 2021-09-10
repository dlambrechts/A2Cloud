using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
   public class BitacoraBLL
    {
        BitacoraMPP mppBit = new BitacoraMPP();
        public void Registrar(BitacoraBE registro)

        {
            registro.FechaCreacion = DateTime.Now;
            mppBit.Registrar(registro);
        }

        public List<BitacoraBE> ListarTodos() 
        
        {
            return mppBit.ListarTodos();
        }
    }
}
