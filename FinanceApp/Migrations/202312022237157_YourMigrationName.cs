namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YourMigrationName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "Currency", "dbo.Balances");
            DropForeignKey("dbo.Incomes", "Currency", "dbo.Balances");
            DropIndex("dbo.Expenses", new[] { "Currency" });
            DropIndex("dbo.Incomes", new[] { "Currency" });
            AddColumn("dbo.Expenses", "BalanceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Expenses", "Currency", c => c.String());
            AlterColumn("dbo.Incomes", "Currency", c => c.String());
            DropTable("dbo.Balances");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        Currency = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Currency);
            
            AlterColumn("dbo.Incomes", "Currency", c => c.String(maxLength: 128));
            AlterColumn("dbo.Expenses", "Currency", c => c.String(maxLength: 128));
            DropColumn("dbo.Expenses", "BalanceId");
            CreateIndex("dbo.Incomes", "Currency");
            CreateIndex("dbo.Expenses", "Currency");
            AddForeignKey("dbo.Incomes", "Currency", "dbo.Balances", "Currency");
            AddForeignKey("dbo.Expenses", "Currency", "dbo.Balances", "Currency");
        }
    }
}
