using MagazineApp.DomainExtension.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.DALExtension.AppDbContext {
    public class AuditDbContext : DbContext {

        public AuditDbContext()
            : base("AuditDbConnection") {

        }

        public AuditDbContext(string connectionString)
            : base(connectionString) {

        }

        public DbSet<AuditArticle> AuditArticles { get; set; }
        public DbSet<AuditMagazine> AuditMagazines { get; set; }

        public static AuditDbContext Create() {
            return new AuditDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
        }
    }
}
