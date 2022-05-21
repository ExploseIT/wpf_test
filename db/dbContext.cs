using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wpf_test.models;

namespace wpf_test.db
{
    public class dbContext : DbContext
    {
        private m_datadir? _m_datadir = null;

        public DbSet<c_safehash> lSH { get; set; }


        public dbContext(m_datadir mDataDir)
        {
            _m_datadir = mDataDir;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(_m_datadir!.getDBConnection());
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<c_safehash>(
            eb =>
            {
                eb.HasNoKey();
            });
        }
    }

}
