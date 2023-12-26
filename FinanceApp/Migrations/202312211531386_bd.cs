namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.Transactions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        Date = c.DateTime(nullable: false),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Users");
        }
    }
}
