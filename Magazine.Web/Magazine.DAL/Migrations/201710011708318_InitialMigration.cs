namespace MagazineApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Caption = c.String(),
                        ShortText = c.String(),
                        LongText = c.String(),
                        AuthorId = c.Guid(nullable: false),
                        MainPictureId = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Magazine_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.MainPictureId, cascadeDelete: true)
                .ForeignKey("dbo.Magazines", t => t.Magazine_Id)
                .Index(t => t.AuthorId)
                .Index(t => t.MainPictureId)
                .Index(t => t.Magazine_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Name = c.String(),
                        Surname = c.String(),
                        IsBlocked = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.Binary(),
                        Description = c.String(),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Magazines",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        MainPictureId = c.Guid(),
                        IsPublished = c.Boolean(),
                        PublisherId = c.Guid(),
                        PublishDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.MainPictureId)
                .ForeignKey("dbo.AspNetUsers", t => t.PublisherId)
                .Index(t => t.MainPictureId)
                .Index(t => t.PublisherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Magazines", "PublisherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Magazines", "MainPictureId", "dbo.Pictures");
            DropForeignKey("dbo.Articles", "Magazine_Id", "dbo.Magazines");
            DropForeignKey("dbo.Articles", "MainPictureId", "dbo.Pictures");
            DropForeignKey("dbo.Articles", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Magazines", new[] { "PublisherId" });
            DropIndex("dbo.Magazines", new[] { "MainPictureId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Articles", new[] { "Magazine_Id" });
            DropIndex("dbo.Articles", new[] { "MainPictureId" });
            DropIndex("dbo.Articles", new[] { "AuthorId" });
            DropTable("dbo.Magazines");
            DropTable("dbo.Pictures");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Articles");
        }
    }
}
