namespace SilberScarves.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisFeaturedtoscarf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scarves", "isFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scarves", "isFeatured");
        }
    }
}
