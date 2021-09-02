using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class PerfilBLL
    {
        PerfilMPP mppPerfil = new PerfilMPP();
        public IList<PerfilPatenteBE> ObtenerPatentes() // Traigo todas las patentes   
        {
           
            return mppPerfil.ObtenerPatentes();
        }

        public Array ObtenerPatentesAtomicas() // Traigo las Patentes Atómicas
        {
            return Enum.GetValues(typeof(PerfilPermisoBE));
        }

        public IList<PerfilFamiliaBE> ObtenerFamilias() // Traigo todas las Familias    
       
        {         
            return mppPerfil.ObtenerFamilias();
        }

        public void GuardarFamilia(PerfilFamiliaBE Fam)

        {  
            mppPerfil.GuardarFamilia(Fam);
        }

        public void GuardarComponente(PerfilComponenteBE Comp, bool EsFamilia)

        {        
           mppPerfil.GuardarComponente(Comp, EsFamilia);
        }

        public IList<PerfilComponenteBE> ObtenerTodo(PerfilFamiliaBE Fam)

        {
            return mppPerfil.ObtenerTodo(Fam);
        }

        public PerfilFamiliaBE ObtenerFamiliaPorId (PerfilFamiliaBE fam)
        
        {
            return mppPerfil.ObtenerFamiliaPorId(fam);
        
        }


        public PerfilFamiliaBE CompletarFamilia (PerfilFamiliaBE fam)

        {

            IList<PerfilComponenteBE> flia = null;

            flia = ObtenerTodo(fam);  
            foreach (var i in flia)
            fam.AgregarHijo(i);

            return fam;
        }
        //public void CompletarComponentesFamilia(PerfilFamiliaBE Familia)

        //{
        //    mppPerfil.CompletarComponentesFamilia(Familia);
        //}

        public void CargarPerfilUsuario(UsuarioBE Us)

        {

            mppPerfil.CargarPerfilUsuario(Us);
        }

        public bool Existe(PerfilComponenteBE Comp, int id)
        {
            bool existe = false;

            if (Comp.Id.Equals(id))
                existe = true;
            else

                foreach (var item in Comp.Hijos)
                {

                    existe = Existe(item, id);
                    if (existe) return true;
                }

            return existe;

        }


    }
}

