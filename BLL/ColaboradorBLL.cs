using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class ColaboradorBLL
    {
        ColaboradorMPP mppColaborador = new ColaboradorMPP();

        public void Insertar(ColaboradorBE Colaborador)

        {
            Colaborador.FechaCreacion = DateTime.Now;

            mppColaborador.Insertar(Colaborador);

        }

        public List<ColaboradorBE> Listar()

        {
            return mppColaborador.Listar();
        }


        public void Editar(ColaboradorBE Colaborador)

        {
            Colaborador.FechaModificacion = DateTime.Now;

            mppColaborador.Editar(Colaborador);
        }

        public ColaboradorBE ObtenerUno(ColaboradorBE Colaborador)

        {
            return mppColaborador.ObtenerUno(Colaborador);
        }

        //public void Eliminar(ColaboradorBE Colaborador)

        //{
        //    Colaborador.FechaModificacion = DateTime.Now;

        //    mppColaborador.Eliminar(Colaborador);

        //}

    }
}

