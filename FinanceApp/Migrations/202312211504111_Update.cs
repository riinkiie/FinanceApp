namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Incomes", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Incomes", "UserId");
            DropColumn("dbo.Expenses", "UserId");
        }
    }
}
