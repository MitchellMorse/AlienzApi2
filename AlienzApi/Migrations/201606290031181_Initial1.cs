namespace AlienzApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        World = c.Int(nullable: false),
                        SequenceInWorld = c.Int(nullable: false),
                        Tier2Score = c.Int(nullable: false),
                        Tier2Reward = c.Int(nullable: false),
                        Tier3Score = c.Int(nullable: false),
                        Tier3Reward = c.Int(nullable: false),
                        Tier1Reward = c.Int(nullable: false),
                        StartingFuel = c.Int(nullable: false),
                        StartingTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Levels");
        }
    }
}
