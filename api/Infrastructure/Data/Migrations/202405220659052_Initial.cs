namespace DigitalMigration.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HeatingProcedure",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FoodName = c.String(nullable: false),
                        Time = c.Int(nullable: false),
                        PowerLevel = c.Int(nullable: false),
                        HeatingString = c.String(nullable: false, maxLength: 1),
                        Instructions = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.HeatingString, unique: true, name: "HeatingStringIndex");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.HeatingProcedure", "HeatingStringIndex");
            DropTable("dbo.HeatingProcedure");
        }
    }
}
