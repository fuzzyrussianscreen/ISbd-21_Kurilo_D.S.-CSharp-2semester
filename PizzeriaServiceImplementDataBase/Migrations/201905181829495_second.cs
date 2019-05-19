namespace PizzeriaServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LetterInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LetterId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Customers", "Post", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LetterInfoes", "CustomerId", "dbo.Customers");
            DropIndex("dbo.LetterInfoes", new[] { "CustomerId" });
            DropColumn("dbo.Customers", "Post");
            DropTable("dbo.LetterInfoes");
        }
    }
}
