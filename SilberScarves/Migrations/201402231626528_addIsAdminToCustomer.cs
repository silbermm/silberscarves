namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsAdminToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "isAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "isAdmin");
        }
    }
}
