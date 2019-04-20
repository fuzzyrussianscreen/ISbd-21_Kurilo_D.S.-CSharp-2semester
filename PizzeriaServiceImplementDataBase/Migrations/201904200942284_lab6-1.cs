namespace PizzeriaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lab61 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Indents", "PerformerId", "dbo.Performers");
            DropIndex("dbo.Indents", new[] { "PerformerId" });
            AlterColumn("dbo.Indents", "PerformerId", c => c.Int());
            CreateIndex("dbo.Indents", "PerformerId");
            AddForeignKey("dbo.Indents", "PerformerId", "dbo.Performers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indents", "PerformerId", "dbo.Performers");
            DropIndex("dbo.Indents", new[] { "PerformerId" });
            AlterColumn("dbo.Indents", "PerformerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Indents", "PerformerId");
            AddForeignKey("dbo.Indents", "PerformerId", "dbo.Performers", "Id", cascadeDelete: true);
        }
    }
}
