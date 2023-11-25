namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Incomes", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Incomes", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Incomes", "Data", c => c.DateTime(nullable: false));
            DropColumn("dbo.Incomes", "Date");
        }
    }
}
