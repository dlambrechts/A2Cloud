using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;

namespace BLL
{
    public class BackupBLL
    {
        BackupMPP mppBak = new BackupMPP();
        public List<BackupBE> ListarBackups()
        {
            return mppBak.ListarBackups();
        }

        public void NuevoBackup(BackupBE back) 
        
        {
            mppBak.NuevoBackup(back);
        }

        public void RestaurarDb(BackupBE back)

        {
            mppBak.RestaurarDb(back);

        }
    }
}
