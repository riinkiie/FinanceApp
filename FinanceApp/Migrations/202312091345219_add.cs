namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Expenses", "BalanceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "BalanceId", c => c.Int(nullable: false));
        }
    }
}
