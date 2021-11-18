using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class AsignacionLicenciaBLL
    {
        AsignacionLicenciaMPP mppAsignacion = new AsignacionLicenciaMPP();

        public void Insertar(AsignacionLicenciaBE Asignacion)

        {
            Asignacion.FechaCreacion = DateTime.Now;

            Asignacion.Estado.Id = 1;

            mppAsignacion.Insertar(Asignacion);


        }

        public void Finalizar(AsignacionLicenciaBE Asignacion)

        {
            Asignacion.FechaModificacion = DateTime.Now;



            mppAsignacion.Finalizar(Asignacion);



        }
        public List<AsignacionLicenciaBE> Listar()

        {

            return mppAsignacion.Listar();

        }

    }
}
