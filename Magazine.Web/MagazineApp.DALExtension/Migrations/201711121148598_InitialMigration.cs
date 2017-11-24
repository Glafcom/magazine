namespace MagazineApp.DALExtension.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditArticles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Caption = c.String(),
                        ShortText = c.String(),
                        LongText = c.String(),
                        MainPicture = c.Binary(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateById = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuditMagazines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                        MainPicture = c.Binary(),
                        IsPublished = c.Boolean(),
                        PublishDate = c.DateTime(),
                        PublisherId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        CreateById = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditMagazines");
            DropTable("dbo.AuditArticles");
        }
    }
}
