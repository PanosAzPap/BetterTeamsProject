namespace ProjectBetterTeams.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        UsernameSender = c.String(nullable: false, maxLength: 20, unicode: false),
                        Receiver = c.String(nullable: false, maxLength: 20, unicode: false),
                        Message = c.String(nullable: false, maxLength: 250, unicode: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageID })
                .ForeignKey("dbo.Users", t => t.UsernameSender)
                .Index(t => t.UsernameSender);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 20, unicode: false),
                        Password = c.String(nullable: false, maxLength: 200, unicode: false),
                        FirstName = c.String(nullable: false, maxLength: 20, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 20, unicode: false),
                        DateOFBirth = c.DateTime(nullable: false, storeType: "date"),
                        UserType = c.String(nullable: false, maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        UsernameSender = c.String(nullable: false, maxLength: 20, unicode: false),
                        DateTime = c.DateTime(nullable: false),
                        Post = c.String(nullable: false, maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => new { t.PostID })
                .ForeignKey("dbo.Users", t => t.UsernameSender)
                .Index(t => t.UsernameSender);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UsernameSender", "dbo.Users");
            DropForeignKey("dbo.Messages", "UsernameSender", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UsernameSender" });
            DropIndex("dbo.Messages", new[] { "UsernameSender" });
            DropTable("dbo.Posts");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
