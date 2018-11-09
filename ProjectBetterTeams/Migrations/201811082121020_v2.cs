namespace ProjectBetterTeams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Messages");
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Messages", "MessageID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Posts", "PostID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Messages", "MessageID");
            AddPrimaryKey("dbo.Posts", "PostID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Posts");
            DropPrimaryKey("dbo.Messages");
            AlterColumn("dbo.Posts", "PostID", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "MessageID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Posts", new[] { "PostID", "UsernameSender" });
            AddPrimaryKey("dbo.Messages", new[] { "MessageID", "UsernameSender" });
        }
    }
}
