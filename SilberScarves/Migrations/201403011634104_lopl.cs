namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lopl : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Addresses", newName: "Address");
            DropForeignKey("dbo.Customers", "address_addressId", "dbo.Addresses");
            DropIndex("dbo.Customers", new[] { "address_addressId" });
            DropColumn("dbo.Customers", "addressId");
            RenameColumn(table: "dbo.Customers", name: "address_addressId", newName: "addressId");
            AlterColumn("dbo.Customers", "addressId", c => c.Long(nullable: false));
            AlterColumn("dbo.Customers", "addressId", c => c.Long(nullable: false));
            CreateIndex("dbo.Customers", "addressId");
            AddForeignKey("dbo.Customers", "addressId", "dbo.Address", "addressId", cascadeDelete: true);
            DropTable("dbo.Admins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            DropForeignKey("dbo.Customers", "addressId", "dbo.Address");
            DropIndex("dbo.Customers", new[] { "addressId" });
            AlterColumn("dbo.Customers", "addressId", c => c.Long());
            AlterColumn("dbo.Customers", "addressId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Customers", name: "addressId", newName: "address_addressId");
            AddColumn("dbo.Customers", "addressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "address_addressId");
            AddForeignKey("dbo.Customers", "address_addressId", "dbo.Addresses", "addressId");
            RenameTable(name: "dbo.Address", newName: "Addresses");
        }
    }
}
