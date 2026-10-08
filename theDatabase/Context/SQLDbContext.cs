using DuckDB.EFCoreProvider.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theInfrastructure;

namespace theDatabase
{
    public class SQLDbContext : DbContext, IDisposable, IAsyncDisposable
    {
        public string DataBaseFileName { get; private set; } =
            ((SqlDbService.Configuration.DatabaseTyp == "SQLite" ? "Default.db" : "Default.duckdb"));

        bool useIntegratedSecurity = false;

        public SQLDbContext(string filepath, bool uis = false)
        {
            DataBaseFileName = filepath;
            useIntegratedSecurity = uis;
        }

        // Configuration 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                // TODO: Hier wird der Pfad zur Datenbank gesetzt 
                string fullFilePath = Path.Combine(SqlDbService.ContentRootPath, "Database", DataBaseFileName);

                // Directory 
                string directoryPath = Path.GetDirectoryName(fullFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    if (!string.IsNullOrEmpty(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                }

                // neue Variante 

                if (SqlDbService.Configuration.DatabaseTyp == "SQLite")
                {
                    optionsBuilder.UseSqlite("Data Source=" + fullFilePath);
                }
                else if (SqlDbService.Configuration.DatabaseTyp == "DuckDB")
                {
                    // optionsBuilder.UseSqlite("Data Source=" + fullFilePath);
                    optionsBuilder.UseDuckDB("Data Source="+ fullFilePath);
                }

                base.OnConfiguring(optionsBuilder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FAIL : " + ex.Message);
            }
        }

        // Events 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // SQLite 
            #region SQLite
            // Content 
            modelBuilder.Entity<tbl_CON_Content>(entity =>
            {
                // Sagt EF Core, dass diese Liste als JSON-Objekt in der SQLite DB abgelegt wird
                entity.OwnsMany(e => e.Properties, builder =>
                {
                    builder.ToJson();
                });
                //entity.OwnsMany(e => e.History, builder =>
                //{
                //    builder.ToJson();
                //});

                entity.OwnsOne(x => x.Metadata, builder =>
                {
                    builder.ToJson();
                });
            });

            // Relation 
            modelBuilder.Entity<tbl_TEC_Relation>()
           .HasKey(c => new
           {
               c.MasterGUID,
               c.RelationType,
               c.ParentGUID,
               c.ChildGUID
           });
            #endregion
        }

        // Tables 
        public DbSet<tbl_CON_Content> tbl_CON_Content { get; set; } // Content Items 
        public DbSet<tbl_TEC_Relation> tbl_TEC_Relation { get; set; } // Relations 
    }

  
}
