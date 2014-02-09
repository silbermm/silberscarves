namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        addressId = c.Long(nullable: false, identity: true),
                        buildingNumber = c.String(),
                        street = c.String(),
                        apartmentNumber = c.String(),
                        city = c.String(),
                        stateCode = c.String(),
                        zipCode = c.String(),
                    })
                .PrimaryKey(t => t.addressId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customerId = c.Long(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        addressId = c.Int(nullable: false),
                        phone = c.String(),
                        address_addressId = c.Long(),
                    })
                .PrimaryKey(t => t.customerId)
                .ForeignKey("dbo.Addresses", t => t.address_addressId)
                .Index(t => t.address_addressId);
            
            CreateTable(
                "dbo.Scarves",
                c => new
                    {
                        scarfId = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.scarfId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "address_addressId", "dbo.Addresses");
            DropIndex("dbo.Customers", new[] { "address_addressId" });
            DropTable("dbo.Scarves");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
