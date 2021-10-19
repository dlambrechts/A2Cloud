using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class DepartamentoBLL
    {
        DepartamentoMPP mppDepartamento = new DepartamentoMPP();

        public void Insertar(DepartamentoBE Departamento)

        {
            Departamento.FechaCreacion = DateTime.Now;

            mppDepartamento.Insertar(Departamento);

        }

        public void Editar(DepartamentoBE Departamento)

        {
            Departamento.FechaModificacion = DateTime.Now;

            mppDepartamento.Editar(Departamento);
        }

        public List<DepartamentoBE> Listar()

        {
            return mppDepartamento.Listar();
        }

        public DepartamentoBE ObtenerUno(DepartamentoBE Departamento)

        {
            return mppDepartamento.ObtenerUno(Departamento);
        }

        public void Eliminar(DepartamentoBE Departamento)

        {
            Departamento.FechaModificacion = DateTime.Now;

            mppDepartamento.Eliminar(Departamento);

        }

    }
}
