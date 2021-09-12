using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Seguridad;
using MPP;


namespace BLL
{
    public class DigitoVerificadorBLL
    {

        DigitoVerificadorMPP mppDV = new DigitoVerificadorMPP();
        BitacoraBLL bllBit = new BitacoraBLL();


        public string CalcularDigitoHorizontal(UsuarioBE User)
        {
            int DVH = 0;

            DVH = GenerarAscii(User.Nombre, 1) + GenerarAscii(User.Apellido, 2) + GenerarAscii(User.Credencial.Mail, 3);

            return Encriptado.Hash(DVH.ToString());
        }

        public string CalcularDigitoVertical(List<UsuarioBE> Users)
        {
            var DVV = new StringBuilder();

            foreach (UsuarioBE item in Users)

            {
                DVV.Append(item.DigitoHorizontal);

            }

            return Encriptado.Hash(DVV.ToString());
        }

        public void ActualizarDigitoVertical(string Dv)
        {

            mppDV.ActualizarVertical(Dv);
        }

        public int GenerarAscii(string atributo, int pos)

        {
            int valor = 0;
            int flag = 1;
            byte[] ValoresASCII = Encoding.ASCII.GetBytes(atributo);
            foreach (byte b in ValoresASCII)
            {
                valor = valor + (b * flag);
                flag++;
            }

            return valor + pos;
        }

        public bool VerificarIntegridadHorizonal(UsuarioBE usuarioEjec)
        {
            UsuarioBLL ubll = new UsuarioBLL();
            List<UsuarioBE> Users = new List<UsuarioBE>(ubll.ListarTodos());

            bool resultado = true;

            foreach (UsuarioBE u in Users)
            {
                string dvh = CalcularDigitoHorizontal(u);

                if (u.DigitoHorizontal != dvh)
                {

                    BitacoraBE RegistroDVV = new BitacoraBE(usuarioEjec);
                    RegistroDVV.Detalle = "La comprobación de DVH detectó inconsistencias en el usuario: " + u.Credencial.Mail;
                    bllBit.Registrar(RegistroDVV);

                    resultado=false;

                }

              
            }

            if (resultado)

            {

                BitacoraBE RegistroDVV = new BitacoraBE(usuarioEjec);
                RegistroDVV.Detalle = "La comprobación de DVH no detectó inconsistencias en la tabla Usuario";
                bllBit.Registrar(RegistroDVV);

            }

            return resultado;

        }

        public bool VerificarIntegridadVertical(UsuarioBE usuarioEjec)

        {

            UsuarioBLL BLLu = new UsuarioBLL();
            string dvv = CalcularDigitoVertical(BLLu.ListarTodos());

            string dvv_db = mppDV.ObtenerVertical();

            if (!dvv.Equals(dvv_db))
            {
                BitacoraBE RegistroDVV= new BitacoraBE(usuarioEjec);
                RegistroDVV.Detalle = "La comprobación de DVV detectó inconsistencias en la tabla Usuario";
                bllBit.Registrar(RegistroDVV);

                return false;

            }

            else 
            
            {
                BitacoraBE RegistroDVV = new BitacoraBE(usuarioEjec);
                RegistroDVV.Detalle = "La comprobación de DVV no detectó inconsistencias en la tabla Usuario";
                bllBit.Registrar(RegistroDVV);

                return true;
            }

        }

        public bool VerificarIntegridad(UsuarioBE usuarioEjec)

        {

            BitacoraBE RegistroInicoVerificacion = new BitacoraBE(usuarioEjec);
            RegistroInicoVerificacion.Detalle = "Inicio de Verificación de Integridad de Datos";
            bllBit.Registrar(RegistroInicoVerificacion);
 


            bool ResultadoVertical = VerificarIntegridadVertical(usuarioEjec);
            bool ResultadoHorizontal = VerificarIntegridadHorizonal(usuarioEjec);

            BitacoraBE RegistroFinVerificacion = new BitacoraBE(usuarioEjec);
            RegistroFinVerificacion.Detalle = "Fin de Verificación de Integridad de Datos";
            bllBit.Registrar(RegistroFinVerificacion);

            if (!ResultadoHorizontal || !ResultadoVertical)

            {
                return false;
            }

            else return true;
        }

    }
}
