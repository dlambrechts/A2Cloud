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

       public FileInfo[] ListarBackups() 
        
        {

            string Path= @"C:\Backup\";

            DirectoryInfo Carpeta = new DirectoryInfo(Path);
            FileInfo[] Backups = Carpeta.GetFiles("*.bak");

            return Backups;

        }

    }
}
