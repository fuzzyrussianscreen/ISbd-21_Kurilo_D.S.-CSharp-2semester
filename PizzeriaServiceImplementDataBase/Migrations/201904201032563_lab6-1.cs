namespace PizzeriaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lab61 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Indents", name: "Performer1Id", newName: "PerformerId");
            RenameIndex(table: "dbo.Indents", name: "IX_Performer1Id", newName: "IX_PerformerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Indents", name: "IX_PerformerId", newName: "IX_Performer1Id");
            RenameColumn(table: "dbo.Indents", name: "PerformerId", newName: "Performer1Id");
        }
    }
}
