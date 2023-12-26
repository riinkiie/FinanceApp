namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "Currency", "dbo.Balances");
            DropForeignKey("dbo.Incomes", "Currency", "dbo.Balances");
            DropIndex("dbo.Expenses", new[] { "Currency" });
            DropIndex("dbo.Incomes", new[] { "Currency" });
            AddColumn("dbo.Expenses", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Expenses", "BalanceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Expenses", "Currency", c => c.String());
            AlterColumn("dbo.Incomes", "Currency", c => c.String());
            DropColumn("dbo.Expenses", "Data");
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
            
            AddColumn("dbo.Expenses", "Data", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Incomes", "Currency", c => c.String(maxLength: 128));
            AlterColumn("dbo.Expenses", "Currency", c => c.String(maxLength: 128));
            DropColumn("dbo.Expenses", "BalanceId");
            DropColumn("dbo.Expenses", "Date");
            CreateIndex("dbo.Incomes", "Currency");
            CreateIndex("dbo.Expenses", "Currency");
            AddForeignKey("dbo.Incomes", "Currency", "dbo.Balances", "Currency");
            AddForeignKey("dbo.Expenses", "Currency", "dbo.Balances", "Currency");
        }
    }
}
