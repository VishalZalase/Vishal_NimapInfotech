namespace Vishal_NimapInfotech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CFADBContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryMsts",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductMsts",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.CategoryMsts", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMsts", "CategoryId", "dbo.CategoryMsts");
            DropIndex("dbo.ProductMsts", new[] { "CategoryId" });
            DropTable("dbo.ProductMsts");
            DropTable("dbo.CategoryMsts");
        }
    }
}
