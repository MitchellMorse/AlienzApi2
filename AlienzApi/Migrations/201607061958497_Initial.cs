namespace AlienzApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AwardReasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
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
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        World = c.Int(nullable: false),
                        SequenceInWorld = c.Int(nullable: false),
                        StartingFuel = c.Int(nullable: false),
                        StartingTime = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        IsBlockingLevel = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Completed = c.Boolean(nullable: false),
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
                        LevelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId)
                .ForeignKey("dbo.Powerups", t => t.PowerupId)
                .Index(t => t.PowerupId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Powerups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Genre = c.String(),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.PlayerDeaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IrreleventTime = c.DateTime(nullable: false),
                        LevelAttemptId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LevelAttempts", t => t.LevelAttemptId)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .Index(t => t.LevelAttemptId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerDeaths", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerDeaths", "LevelAttemptId", "dbo.LevelAttempts");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.TierScoreRewards", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.PlayerEnergyPackagePurchases", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerEnergyPackagePurchases", "EnergyPackageId", "dbo.EnergyPackages");
            DropForeignKey("dbo.PlayerEnergyPackageAwards", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerEnergyPackageAwards", "AwardReasonId", "dbo.AwardReasons");
            DropForeignKey("dbo.LevelAttempts", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.EnergyPurchases", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.EnergyPurchases", "EnergyPurchaseableItemId", "dbo.EnergyPurchaseableItems");
            DropForeignKey("dbo.EnergyPurchaseableItems", "PowerupId", "dbo.Powerups");
            DropForeignKey("dbo.PlayerPowerupUsages", "PowerupId", "dbo.Powerups");
            DropForeignKey("dbo.PlayerPowerupUsages", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerPowerupUsages", "LevelAttemptId", "dbo.LevelAttempts");
            DropForeignKey("dbo.EnergyPurchaseableItems", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.AdViews", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.LevelAttempts", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.AwardReasons", "TierScoreRewardId", "dbo.TierScoreRewards");
            DropIndex("dbo.PlayerDeaths", new[] { "PlayerId" });
            DropIndex("dbo.PlayerDeaths", new[] { "LevelAttemptId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.PlayerEnergyPackagePurchases", new[] { "EnergyPackageId" });
            DropIndex("dbo.PlayerEnergyPackagePurchases", new[] { "PlayerId" });
            DropIndex("dbo.PlayerEnergyPackageAwards", new[] { "AwardReasonId" });
            DropIndex("dbo.PlayerEnergyPackageAwards", new[] { "PlayerId" });
            DropIndex("dbo.PlayerPowerupUsages", new[] { "LevelAttemptId" });
            DropIndex("dbo.PlayerPowerupUsages", new[] { "PowerupId" });
            DropIndex("dbo.PlayerPowerupUsages", new[] { "PlayerId" });
            DropIndex("dbo.EnergyPurchaseableItems", new[] { "LevelId" });
            DropIndex("dbo.EnergyPurchaseableItems", new[] { "PowerupId" });
            DropIndex("dbo.EnergyPurchases", new[] { "EnergyPurchaseableItemId" });
            DropIndex("dbo.EnergyPurchases", new[] { "PlayerId" });
            DropIndex("dbo.LevelAttempts", new[] { "LevelId" });
            DropIndex("dbo.LevelAttempts", new[] { "PlayerId" });
            DropIndex("dbo.TierScoreRewards", new[] { "LevelId" });
            DropIndex("dbo.AwardReasons", new[] { "TierScoreRewardId" });
            DropIndex("dbo.AdViews", new[] { "PlayerId" });
            DropTable("dbo.PlayerDeaths");
            DropTable("dbo.Books");
            DropTable("dbo.EnergyPackages");
            DropTable("dbo.PlayerEnergyPackagePurchases");
            DropTable("dbo.PlayerEnergyPackageAwards");
            DropTable("dbo.PlayerPowerupUsages");
            DropTable("dbo.Powerups");
            DropTable("dbo.EnergyPurchaseableItems");
            DropTable("dbo.EnergyPurchases");
            DropTable("dbo.Players");
            DropTable("dbo.LevelAttempts");
            DropTable("dbo.Levels");
            DropTable("dbo.TierScoreRewards");
            DropTable("dbo.AwardReasons");
            DropTable("dbo.Authors");
            DropTable("dbo.AdViews");
        }
    }
}
