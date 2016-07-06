using AlienzApi.Models.ExampleModels;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AlienzApi.Models.AlienzApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AlienzApi.Models.AlienzApiContext context)
        {
            context.Levels.AddOrUpdate(x => x.Id,
                new Level()
                {
                    Id = 1,
                    SequenceInWorld = 1,
                    World = 1,
                    StartingFuel = 100,
                    StartingTime = 2000,
                    Tier1Reward = 1,
                    Tier2Reward = 1,
                    Tier3Reward = 1,
                    Tier2Score = 200,
                    Tier3Score = 300,
                    Active = true
                },
                new Level()
                {
                    Id = 2,
                    SequenceInWorld = 2,
                    World = 1,
                    StartingFuel = 200,
                    StartingTime = 2000,
                    Tier1Reward = 1,
                    Tier2Reward = 1,
                    Tier3Reward = 1,
                    Tier2Score = 200,
                    Tier3Score = 300,
                    Active = true
                },
                new Level()
                {
                    Id = 3,
                    SequenceInWorld = 3,
                    World = 1,
                    StartingFuel = 100,
                    StartingTime = 2000,
                    Tier1Reward = 1,
                    Tier2Reward = 1,
                    Tier3Reward = 1,
                    Tier2Score = 200,
                    Tier3Score = 300,
                    Active = true
                }
                );

            context.TierScoreRewards.AddOrUpdate(
                new TierScoreReward()
                {
                    Id = 1,
                    TierNumber = 1,
                    Score = 500,
                    LevelId = 1
                },
                new TierScoreReward()
                {
                    Id = 2,
                    TierNumber = 2,
                    Score = 1000,
                    LevelId = 1
                },
                new TierScoreReward()
                {
                    Id = 3,
                    TierNumber = 3,
                    Score = 1200,
                    LevelId = 1
                },
                new TierScoreReward()
                {
                    Id = 4,
                    TierNumber = 1,
                    Score = 800,
                    LevelId = 2
                },
                new TierScoreReward()
                {
                    Id = 5,
                    TierNumber = 2,
                    Score = 1000,
                    LevelId = 2
                },
                new TierScoreReward()
                {
                    Id = 6,
                    TierNumber = 3,
                    Score = 1200,
                    LevelId = 2
                },
                new TierScoreReward()
                {
                    Id = 7,
                    TierNumber = 1,
                    Score = 500,
                    LevelId = 3
                },
                new TierScoreReward()
                {
                    Id = 8,
                    TierNumber = 2,
                    Score = 1000,
                    LevelId = 3
                },
                new TierScoreReward()
                {
                    Id = 9,
                    TierNumber = 3,
                    Score = 1200,
                    LevelId = 3
                }
                );

            context.AwardReasons.AddOrUpdate(
                new AwardReason()
                {
                    Id = 1,
                    Name = "AdView",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = null
                },
                new AwardReason()
                {
                    Id = 2,
                    Name = "InviteFriend",
                    EnergyRewardAmount = 2,
                    Active = true,
                    TierScoreRewardId = null
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 1
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 2
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 3
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 4
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 5
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 6
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 7
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 8
                },
                new AwardReason()
                {
                    Id = 3,
                    Name = "TierCompleted",
                    EnergyRewardAmount = 1,
                    Active = true,
                    TierScoreRewardId = 9
                }
                );

            context.EnergyPackages.AddOrUpdate(
                new EnergyPackage()
                {
                    Id = 1,
                    Amount = 10,
                    Active = true,
                    DollarCost = 0.99f  //9 cents/unit
                },
                new EnergyPackage()
                {
                    Id = 2,
                    Amount = 50,
                    Active = true,
                    DollarCost = 3.99f //8 cents/unit
                },
                new EnergyPackage()
                {
                    Id = 1,
                    Amount = 100,
                    Active = true,
                    DollarCost = 5.50f // 5.5 cents/unit
                },
                new EnergyPackage()
                {
                    Id = 1,
                    Amount = 300,
                    Active = true,
                    DollarCost = 14.99f //.5 cents/unit
                }
                );

            context.BlockWalls.AddOrUpdate(
                new BlockWall()
                {
                    Id = 1,
                    World = 1,
                    Sequence = 4
                }
                );

            context.Powerups.AddOrUpdate(
                new Powerup()
                {
                    Id = 1,
                    Name = "Speed"
                },
                new Powerup()
                {
                    Id = 2,
                    Name = "Jump"
                }
                );

            context.EnergyPurchaseableItems.AddOrUpdate(
                new EnergyPurchaseableItem()
                {
                    Id = 1,
                    Name = "BlockWall",
                    PowerupId = null,
                    Quantity = 1,
                    EnergyCost = 5,
                    Active = true,
                    BlockWallId = 1
                },
                new EnergyPurchaseableItem()
                {
                    Id = 2,
                    Name = "PowerupSpeed",
                    PowerupId = 1,
                    Quantity = 3,
                    EnergyCost = 10,
                    Active = true,
                    BlockWallId = null
                },
                new EnergyPurchaseableItem()
                {
                    Id = 3,
                    Name = "PowerupJump",
                    PowerupId = 2,
                    Quantity = 3,
                    EnergyCost = 10,
                    Active = true,
                    BlockWallId = null
                },
                new EnergyPurchaseableItem()
                {
                    Id = 4,
                    Name = "PowerupSpeed",
                    PowerupId = 1,
                    Quantity = 6,
                    EnergyCost = 18,
                    Active = true,
                    BlockWallId = null
                },
                new EnergyPurchaseableItem()
                {
                    Id = 5,
                    Name = "PowerupJump",
                    PowerupId = 1,
                    Quantity = 6,
                    EnergyCost = 18,
                    Active = true,
                    BlockWallId = null
                }
                );
        }
    }
}
