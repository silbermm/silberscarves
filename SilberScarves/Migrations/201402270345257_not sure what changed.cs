namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notsurewhatchanged : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ScarfOrderScarfItems", newName: "ScarfItemScarfOrders");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ScarfItemScarfOrders", newName: "ScarfOrderScarfItems");
        }
    }
}
