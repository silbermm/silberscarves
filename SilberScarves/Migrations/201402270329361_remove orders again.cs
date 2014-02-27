namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeordersagain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderId = c.Long(nullable: false, identity: true),
                        isCart = c.Boolean(nullable: false),
                        hasShipped = c.Boolean(nullable: false),
                        hasBeenPaidFor = c.Boolean(nullable: false),
                        customer_customerId = c.Long(),
                    })
                .PrimaryKey(t => t.orderId)
                .ForeignKey("dbo.Customers", t => t.customer_customerId)
                .Index(t => t.customer_customerId);
            
            CreateTable(
                "dbo.ScarfOrderScarfItems",
                c => new
                    {
                        ScarfOrder_orderId = c.Long(nullable: false),
                        ScarfItem_scarfId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ScarfOrder_orderId, t.ScarfItem_scarfId })
                .ForeignKey("dbo.Orders", t => t.ScarfOrder_orderId, cascadeDelete: true)
                .ForeignKey("dbo.Scarves", t => t.ScarfItem_scarfId, cascadeDelete: true)
                .Index(t => t.ScarfOrder_orderId)
                .Index(t => t.ScarfItem_scarfId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScarfOrderScarfItems", "ScarfItem_scarfId", "dbo.Scarves");
            DropForeignKey("dbo.ScarfOrderScarfItems", "ScarfOrder_orderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "customer_customerId", "dbo.Customers");
            DropIndex("dbo.ScarfOrderScarfItems", new[] { "ScarfItem_scarfId" });
            DropIndex("dbo.ScarfOrderScarfItems", new[] { "ScarfOrder_orderId" });
            DropIndex("dbo.Orders", new[] { "customer_customerId" });
            DropTable("dbo.ScarfOrderScarfItems");
            DropTable("dbo.Orders");
        }
    }
}
