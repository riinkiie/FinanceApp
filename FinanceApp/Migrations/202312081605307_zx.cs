namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zx : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Data", c => c.DateTime(nullable: false));
            DropColumn("dbo.Expenses", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Expenses", "Data");
        }
    }
}
