namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CanSendMessages_users : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CanSend", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CanSend");
        }
    }
}
