namespace MagazineApp.DAL.Migrations
{
    using Domain.Entities.Identity;
    using Domain.Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MagazineApp.DAL.AppDbContext.MagazineAppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MagazineApp.DAL.AppDbContext.MagazineAppDbContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Role { Name = UserType.Admin.ToString() },
                new Role { Name = UserType.Journalist.ToString() },
                new Role { Name = UserType.Editor.ToString() }

             );

            var userStore = new UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>(context);
            var userManager = new UserManager<User, Guid>(userStore);

            if (!context.Users.Any(u => u.UserName == "SuperAdmin")) {
                var admin = new User {
                    Email = "little-beetle_88@mail.ru",
                    UserName = "SuperAdmin",
                    Name = "Super",
                    Surname = "Admin"
                };

                userManager.Create(admin, "admin123");
                userManager.AddToRole(admin.Id, UserType.Admin.ToString());
            }

            context.SaveChanges();
        }
    }
}
