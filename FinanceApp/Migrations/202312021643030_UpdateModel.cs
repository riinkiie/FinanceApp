namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        Currency = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Currency);
            
            AlterColumn("dbo.Expenses", "Currency", c => c.String(maxLength: 128));
            AlterColumn("dbo.Incomes", "Currency", c => c.String(maxLength: 128));
            CreateIndex("dbo.Expenses", "Currency");
            CreateIndex("dbo.Incomes", "Currency");
            AddForeignKey("dbo.Expenses", "Currency", "dbo.Balances", "Currency");
            AddForeignKey("dbo.Incomes", "Currency", "dbo.Balances", "Currency");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Incomes", "Currency", "dbo.Balances");
            DropForeignKey("dbo.Expenses", "Currency", "dbo.Balances");
            DropIndex("dbo.Incomes", new[] { "Currency" });
            DropIndex("dbo.Expenses", new[] { "Currency" });
            AlterColumn("dbo.Incomes", "Currency", c => c.String());
            AlterColumn("dbo.Expenses", "Currency", c => c.String());
            DropTable("dbo.Balances");
        }
    }
}
