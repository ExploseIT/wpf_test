using wpf_test.db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.models
{
    public class m_safehash
    {
        private dbContext _dbCon;
        private int pageSize = 10;
        public m_safehash(dbContext dbCon)
        {
            this._dbCon = dbCon;
        }

        public List<c_safehash> doReadAll()
        {
            List<c_safehash> ret = new List<c_safehash>();

            ret = _dbCon.lSH.ToList<c_safehash>();
            return ret;
        }

        public List<c_safehash> doReadPage(int m_page, string m_search)
        {
            int c_page = m_page;
            if ((m_search +"").Length >0 || m_page < 1)
            {
                c_page = 1;
            }
            List<c_safehash> ret = new List<c_safehash>();
            ret = _dbCon.lSH.Where(sh=>sh.sha1.ToLower().Contains(m_search.ToLower())).Skip((c_page - 1) * pageSize).Take(pageSize).ToList<c_safehash>();

            return ret;
        }

        public int doGetPageCount(int m_page, string m_search)
        {
            int pc = _dbCon.lSH.Where(sh => sh.sha1.ToLower().Contains(m_search.ToLower())).Count();
            int ret = (pc / pageSize) + 1;
            if (pc % pageSize > 0)
            {
                ret++;
            }
            return ret;
        }

        public void doRaw()
        {
            using (var con = new SQLiteConnection(new m_datadir().getDataDirectory()))
            {
                con.Open();
                string CmdString = @"SELECT * FROM safe_hashes";
                var cmd = new SQLiteCommand(CmdString, con);
                SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);

                var dt = new DataTable("dtSH");

                sda.Fill(dt);



                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hash_id = reader.GetString(0);
                        var sha = reader.GetString(1);

                        Console.WriteLine($"Hash_id: {hash_id}, Sha {sha} !");
                    }
                }
            }
        }
    }

    [Table("safe_hashes")]
    public class c_safehash
    {
        [Key]
        public string hash_id { get; set; }
        public string sha1 { get; set; }
    }
}
