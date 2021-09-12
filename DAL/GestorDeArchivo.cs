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
            FileInfo[] Backups = Carpeta.GetFiles("*.bak");

            return Backups;

        }



    }
}
