using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.models
{
    public class m_datadir
    {
        string _data_dir = "data_dir";
        private string _dbname = "db.sqlite";

        private string? mDataDir = null;
        private string? mDBConnection = null;

        public m_datadir()
        {
            DirectoryInfo? cDir;
            string? fDir = Directory.GetCurrentDirectory();

            while (fDir?.Split('\\', '/').Length > 1)
            {
                var fpath = Directory.EnumerateDirectories(fDir).FirstOrDefault(f => f.ToLower().Contains(_data_dir.ToLower()));
                if (fpath != null)
                {
                    mDataDir = fpath;
                    mDBConnection = Path.Combine(fpath, _dbname);
                    return;
                }
                else
                {
                    cDir = Directory.GetParent(fDir);
                    fDir = cDir?.ToString();
                }
            }
        }


        public string getDataDirectory()
        {
            return mDataDir ?? "";
        }

        public string getDBConnection()
        {
            string ret = $"Data Source={mDBConnection ?? ""}";
            return ret;
        }
    }
}
