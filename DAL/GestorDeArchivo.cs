using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DAL
{
    public class GestorDeArchivo
    {

       public FileInfo[] ListarBackups(string path) 
        
        {

            DirectoryInfo Carpeta = new DirectoryInfo(path);            
            Carpeta.Create();  // Si el directorio no existe, lo creo
            FileInfo[] Backups = Carpeta.GetFiles("*.bak");

            return Backups;

        }

        public void EliminarBackup(string path,string nombreBackup)

        {

            DirectoryInfo Carpeta = new DirectoryInfo(path);
            FileInfo[] Backups = Carpeta.GetFiles("*.bak");

            foreach(FileInfo archivo in Backups) 
            
            { 
                if(archivo.Name==nombreBackup)
                {
                    archivo.Delete();
                }
            }

        }



    }
}
