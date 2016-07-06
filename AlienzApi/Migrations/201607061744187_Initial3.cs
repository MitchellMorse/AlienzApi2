namespace AlienzApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.AwardReasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1),
                        EnergyRewardAmount = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        TierScoreRewardId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TierScoreRewards", t => t.TierScoreRewardId)
                .Index(t => t.TierScoreRewardId);
            
            CreateTable(
                "dbo.TierScoreRewards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TierNumber = c.Int(nullable: false),
                        Score = c.Long(nullable: false),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.LevelAttempts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        LevelId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TimesDied = c.Int(nullable: false),
                        Score = c.Long(nullable: false),
                        TimeSeconds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EnergyPurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        EnergyPurchaseableItemId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnergyPurchaseableItems", t => t.EnergyPurchaseableItemId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.EnergyPurchaseableItemId);
            
            CreateTable(
                "dbo.EnergyPurchaseableItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PowerupId = c.Int(),
                        EnergyCost = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        BlockWallId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlockWalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        World = c.Int(nullable: false),
                        Sequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Powerups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1),
                        EnergyPurchaseableItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnergyPurchaseableItems", t => t.EnergyPurchaseableItem_Id)
                .Index(t => t.EnergyPurchaseableItem_Id);
            
            CreateTable(
                "dbo.PlayerPowerupUsages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        PowerupId = c.Int(nullable: false),
                        LevelAttemptId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LevelAttempts", t => t.LevelAttemptId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.Powerups", t => t.PowerupId)
                .Index(t => t.PlayerId)
                .Index(t => t.PowerupId)
                .Index(t => t.LevelAttemptId);
            
            CreateTable(
                "dbo.PlayerEnergyPackageAwards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AwardReasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AwardReasons", t => t.AwardReasonId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.AwardReasonId);
            
            CreateTable(
                "dbo.PlayerEnergyPackagePurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        EnergyPackageId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EnergyPackages", t => t.EnergyPackageId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.EnergyPackageId);
            
            CreateTable(
                "dbo.EnergyPackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DollarCost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlockWallEnergyPurchaseableItems",
                c => new
                    {
                        BlockWall_Id = c.Int(nullable: false),
                        EnergyPurchaseableItem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlockWall_Id, t.EnergyPurchaseableItem_Id })
                .ForeignKey("dbo.BlockWalls", t => t.BlockWall_Id, cascadeDelete: true)
                .ForeignKey("dbo.EnergyPurchaseableItems", t => t.EnergyPurchaseableItem_Id, cascadeDelete: true)
                .Index(t => t.BlockWall_Id)
                .Index(t => t.EnergyPurchaseableItem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TierScoreRewards", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.PlayerEnergyPackagePurchases", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerEnergyPackagePurchases", "EnergyPackageId", "dbo.EnergyPackages");
            DropForeignKey("dbo.PlayerEnergyPackageAwards", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerEnergyPackageAwards", "AwardReasonId", "dbo.AwardReasons");
            DropForeignKey("dbo.LevelAttempts", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.EnergyPurchases", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.EnergyPurchases", "EnergyPurchaseableItemId", "dbo.EnergyPurchaseableItems");
            DropForeignKey("dbo.Powerups", "EnergyPurchaseableItem_Id", "dbo.EnergyPurchaseableItems");
            DropForeignKey("dbo.PlayerPowerupUsages", "PowerupId", "dbo.Powerups");
            DropForeignKey("dbo.PlayerPowerupUsages", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerPowerupUsages", "LevelAttemptId", "dbo.LevelAttempts");
            DropForeignKey("dbo.BlockWallEnergyPurchaseableItems", "EnergyPurchaseableItem_Id", "dbo.EnergyPurchaseableItems");
            DropForeignKey("dbo.BlockWallEnergyPurchaseableItems", "BlockWall_Id", "dbo.BlockWalls");
            DropForeignKey("dbo.AdViews", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.LevelAttempts", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.AwardReasons", "TierScoreRewardId", "dbo.TierScoreRewards");
            DropIndex("dbo.BlockWallEnergyPurchaseableItems", new[] { "EnergyPurchaseableItem_Id" });
            DropIndex("dbo.BlockWallEnergyPurchaseableItems", new[] { "BlockWall_Id" });
            DropIndex("dbo.PlayerEnergyPackagePurchases", new[] { "EnergyPackageId" });
            DropIndex("dbo.PlayerEnergyPackagePurchases", new[] { "PlayerId" });
            DropIndex("dbo.PlayerEnergyPackageAwards", new[] { "AwardReasonId" });
            DropIndex("dbo.PlayerEnergyPackageAwards", new[] { "PlayerId" });
            DropIndex("dbo.PlayerPowerupUsages", new[] { "LevelAttemptId" });
            DropIndex("dbo.PlayerPowerupUsages", new[] { "PowerupId" });
            DropIndex("dbo.PlayerPowerupUsages", new[] { "PlayerId" });
            DropIndex("dbo.Powerups", new[] { "EnergyPurchaseableItem_Id" });
            DropIndex("dbo.EnergyPurchases", new[] { "EnergyPurchaseableItemId" });
            DropIndex("dbo.EnergyPurchases", new[] { "PlayerId" });
            DropIndex("dbo.LevelAttempts", new[] { "LevelId" });
            DropIndex("dbo.LevelAttempts", new[] { "PlayerId" });
            DropIndex("dbo.TierScoreRewards", new[] { "LevelId" });
            DropIndex("dbo.AwardReasons", new[] { "TierScoreRewardId" });
            DropIndex("dbo.AdViews", new[] { "PlayerId" });
            DropTable("dbo.BlockWallEnergyPurchaseableItems");
            DropTable("dbo.EnergyPackages");
            DropTable("dbo.PlayerEnergyPackagePurchases");
            DropTable("dbo.PlayerEnergyPackageAwards");
            DropTable("dbo.PlayerPowerupUsages");
            DropTable("dbo.Powerups");
            DropTable("dbo.BlockWalls");
            DropTable("dbo.EnergyPurchaseableItems");
            DropTable("dbo.EnergyPurchases");
            DropTable("dbo.Players");
            DropTable("dbo.LevelAttempts");
            DropTable("dbo.TierScoreRewards");
            DropTable("dbo.AwardReasons");
            DropTable("dbo.AdViews");
        }
    }
}
