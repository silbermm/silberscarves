namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolesandusers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        CustomerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Roles_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Roles_Id })
                .ForeignKey("dbo.users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.roles", t => t.Roles_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Roles_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Roles_Id", "dbo.roles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.users");
            DropForeignKey("dbo.users", "CustomerId", "dbo.Customers");
            DropIndex("dbo.UserRoles", new[] { "Roles_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.users", new[] { "CustomerId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.users");
            DropTable("dbo.roles");
        }
    }
}
