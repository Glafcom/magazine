namespace MagazineApp.DALExtension.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMagazineIdToArticleEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditArticles", "MagazineId", c => c.Guid(nullable: false));
            AddColumn("dbo.AuditArticles", "OriginalId", c => c.Guid(nullable: false));
            AddColumn("dbo.AuditMagazines", "OriginalId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditMagazines", "OriginalId");
            DropColumn("dbo.AuditArticles", "OriginalId");
            DropColumn("dbo.AuditArticles", "MagazineId");
        }
    }
}
