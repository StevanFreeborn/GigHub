namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFollowing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "Follower_Id" });
            AlterColumn("dbo.Followings", "Follower_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Followings", "Follower_Id");
            AddForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "Follower_Id" });
            AlterColumn("dbo.Followings", "Follower_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Followings", "Follower_Id");
            AddForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
