namespace PizzeriaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Performers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PerformerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Indents", "PerformerId", c => c.Int());
            CreateIndex("dbo.Indents", "PerformerId");
            AddForeignKey("dbo.Indents", "PerformerId", "dbo.Performers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indents", "PerformerId", "dbo.Performers");
            DropIndex("dbo.Indents", new[] { "PerformerId" });
            DropColumn("dbo.Indents", "PerformerId");
            DropTable("dbo.Performers");
        }
    }
}
