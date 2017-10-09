namespace MagazineApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedPictureEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "MainPictureId", "dbo.Pictures");
            DropForeignKey("dbo.Magazines", "MainPictureId", "dbo.Pictures");
            DropIndex("dbo.Articles", new[] { "MainPictureId" });
            DropIndex("dbo.Magazines", new[] { "MainPictureId" });
            AddColumn("dbo.Articles", "MainPicture", c => c.Binary());
            AddColumn("dbo.Magazines", "MainPicture", c => c.Binary());
            DropColumn("dbo.Articles", "MainPictureId");
            DropColumn("dbo.Magazines", "MainPictureId");
            DropTable("dbo.Pictures");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Magazines", "MainPictureId", c => c.Guid());
            AddColumn("dbo.Articles", "MainPictureId", c => c.Guid(nullable: false));
            DropColumn("dbo.Magazines", "MainPicture");
            DropColumn("dbo.Articles", "MainPicture");
            CreateIndex("dbo.Magazines", "MainPictureId");
            CreateIndex("dbo.Articles", "MainPictureId");
            AddForeignKey("dbo.Magazines", "MainPictureId", "dbo.Pictures", "Id");
            AddForeignKey("dbo.Articles", "MainPictureId", "dbo.Pictures", "Id", cascadeDelete: true);
        }
    }
}
