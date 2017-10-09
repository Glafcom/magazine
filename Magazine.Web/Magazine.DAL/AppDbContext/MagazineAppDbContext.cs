using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.DAL.AppDbContext {
    public class MagazineAppDbContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim> {

        public MagazineAppDbContext()
            : base("MagazineAppDbConnection") {

        }

        public MagazineAppDbContext(string connectionString)
            : base(connectionString) {
            
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Magazine> Magazines { get; set; }

        public static MagazineAppDbContext Create() {
            return new MagazineAppDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
        }


    }
}
