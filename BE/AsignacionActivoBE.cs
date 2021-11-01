﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class AsignacionActivoBE:EntityBE
    {
        private string detalle;
        private ActivoBE activo;
        private ColaboradorBE colaborador;
        private AsignacionEstadoBE estado;
        private DateTime fechaInicio;
        private DateTime fechaFinalizacion;
        private UbicacionBE ubicacion;

        public string Detalle { get => detalle; set => detalle = value; }
        public ActivoBE Activo { get => activo; set => activo = value; }
        public ColaboradorBE Colaborador { get => colaborador; set => colaborador = value; }
        public AsignacionEstadoBE Estado { get => estado; set => estado = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFinalizacion { get => fechaFinalizacion; set => fechaFinalizacion = value; }
        public UbicacionBE Ubicacion { get => ubicacion; set => ubicacion = value; }

        public AsignacionActivoBE() 
        
        {
            activo = new ActivoBE();
            colaborador = new ColaboradorBE();
            estado = new AsignacionEstadoBE();
            ubicacion = new UbicacionBE();
        }
    }
}
