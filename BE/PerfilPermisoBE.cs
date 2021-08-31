using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PerfilPermisoBE
    {
        public enum Permiso
        {
            PermisoA, // ABM Usuarios
            PermisoB, // Gestión de Perfiles de Acceso
            PermisoC, // Asignar Perfiles a Usuarios
            PermisoD, // Puede Emitir Presupuestos
            PermisoE, // Puede realizar Aprobacion Técnica de Presupuesto
            PermisoF, // Puede Realizar Aprobación Comercial de Presupuesto
            PermisoG, // Visualizar Presupuestos
            PermisoH, // Puede Anular Presupuestos
            PermisoJ, // Gestión de Idioma y Traducciones
            PermisoK, // Gestión de Clientes ABM
            PermisoL, // Gestion de Productos ABM
            PermisoM, // Visualiza Indicadores
            PermisoN, // Puede hacer movimientos de stock
            PermisoO, // Gestion de Pedidos
            PermisoP, // Visualiza Bitacora
            PermisoQ, // Gestion de Backup
           
        }
    }
}
