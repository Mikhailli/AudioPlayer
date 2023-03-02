namespace AudioPlayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        Duration = c.Int(nullable: false),
                        PreviousAudio_Id = c.Int(),
                        NextAudio_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Audios", t => t.PreviousAudio_Id)
                .ForeignKey("dbo.Audios", t => t.NextAudio_Id)
                .Index(t => t.PreviousAudio_Id)
                .Index(t => t.NextAudio_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Audios", "NextAudio_Id", "dbo.Audios");
            DropForeignKey("dbo.Audios", "PreviousAudio_Id", "dbo.Audios");
            DropIndex("dbo.Audios", new[] { "NextAudio_Id" });
            DropIndex("dbo.Audios", new[] { "PreviousAudio_Id" });
            DropTable("dbo.Audios");
        }
    }
}
