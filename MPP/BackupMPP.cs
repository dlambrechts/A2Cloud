using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;
using System.IO;
using DAL;

namespace MPP
{
    public class BackupMPP
    {

        GestorDeArchivo Ges = new GestorDeArchivo();
           
        public List<BackupBE>ListarBackups()

        {
            List<BackupBE> lista = new List<BackupBE>();

            FileInfo[] contenido = Ges.ListarBackups();

            foreach(FileInfo archivo in contenido)
            
            {
                BackupBE bak = new BackupBE();
                bak.Nombre = archivo.Name;

                lista.Add(bak);
            }

            return lista;

        }
    }
}
