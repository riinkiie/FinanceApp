namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Expenses", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "Data", c => c.DateTime(nullable: false));
            DropColumn("dbo.Expenses", "Date");
        }
    }
}
