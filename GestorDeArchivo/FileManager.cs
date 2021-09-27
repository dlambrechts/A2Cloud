using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace GestorDeArchivo
{
    public class FileMananager  
    {
        public FileInfo[] ListarBackups(string path)

        {

   
            DirectoryInfo CarpetaBackups = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));
            CarpetaBackups.Create();  // Si el directorio no existe, lo creo
            FileInfo[] Backups = CarpetaBackups.GetFiles("*.bak");

            return Backups;

        }

        public void EliminarBackup(string path, string nombreBackup)

        {
            try
            {

            DirectoryInfo Carpeta = new DirectoryInfo(HttpContext.Current.Server.MapPath(@"\Backup\"));
            FileInfo[] Backups = Carpeta.GetFiles("*.bak");
          
            foreach (FileInfo archivo in Backups)

            {
                if (archivo.Name == nombreBackup)
                {
                    archivo.Delete();
                }
            }
            }

            catch(Exception ex) 
            
            {
                RegistrarError(ex.Message);
            }
        }

            public static void RegistrarError(string MensajeError)
        {
       
                string strFileName = "Error.log";
               
                MensajeError = MensajeError.Insert(0,DateTime.Now.ToString()+" ");

                DirectoryInfo CarpetaErrores = new DirectoryInfo(HttpContext.Current.Server.MapPath(@"\Error\"));

                CarpetaErrores.Create();

                FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", CarpetaErrores, strFileName), FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(MensajeError);
                objStreamWriter.Close();
                objFilestream.Close();


        }

    }
}
