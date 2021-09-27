using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.IO;
using DAL;
using GestorDeArchivo;
using System.Web;

namespace MPP
{
    public class BackupMPP
    {

        FileMananager Ges = new FileMananager();

        string CarpetaBackups = @"\Backup\";
        string DB = "A2Cloud";
           
        public List<BackupBE>ListarBackups()

        {
            List<BackupBE> lista = new List<BackupBE>();

            FileInfo[] contenido = Ges.ListarBackups(CarpetaBackups);

            foreach(FileInfo archivo in contenido)
            
            {
                BackupBE bak = new BackupBE();
                bak.Nombre = archivo.Name;
                bak.FechaCreacion = archivo.CreationTime;

                lista.Add(bak);
            }

            lista = lista.OrderByDescending(l => l.FechaCreacion).ToList();

            return lista;

        }

        public void NuevoBackup(BackupBE back)

        {

            DateTime Fecha = DateTime.Now;
            string Path = CarpetaBackups+back.Nombre+ ".bak";

            string Query = "BACKUP DATABASE " + DB + " TO DISK ='" + HttpContext.Current.Server.MapPath(Path) + "'";

            Acceso AccesoDB = new Acceso();
            AccesoDB.QueryBackup(Query);

        }

        public void EliminarBackup(BackupBE back)

        {

            Ges.EliminarBackup(CarpetaBackups, back.Nombre);

        }

        public void RestaurarDb(BackupBE back)

        {
            string Query = " ALTER DATABASE " + DB + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE " + DB + " RESTORE DATABASE " + DB + "  FROM DISK ='" + HttpContext.Current.Server.MapPath(CarpetaBackups + back.Nombre) + "'";
            Acceso AccesoDB = new Acceso();
            AccesoDB.QueryBackup(Query);
        }


    }
}
