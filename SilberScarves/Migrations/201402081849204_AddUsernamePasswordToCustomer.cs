namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsernamePasswordToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "username", c => c.String());
            AddColumn("dbo.Customers", "password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "password");
            DropColumn("dbo.Customers", "username");
        }
    }
}
