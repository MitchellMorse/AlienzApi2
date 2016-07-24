using System;
using System.Collections.Generic;
using System.Linq;
using AlienzApi.Models;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlienzApi.Tests.TestHelpers
{
    [TestClass]
    public abstract class AlienzTester
    {
        protected const int _testWorldId = 9999;

        protected IAlienzApiContext _context { get; set; }
        protected abstract IAlienzApiContext DbContext { get; }

        private List<int> LevelAttemptIdsToCleanup { get; set; }
        private List<int> LevelIdsToCleanup { get; set; }
        private List<int> PlayerIdsToCleanup { get; set; }
        private List<int> TierScoreRewardIdsToCleanup { get; set; }
        private List<int> AwardReasonIdsToCleanup { get; set; }
        private List<int> PlayerDeathIdsToCleanup { get; set; }
        private List<int> EnergyPurchaseIdsToCleanup { get; set; }
        private List<int> PlayerPowerupUsagesIdsToCleanup { get; set; }

        protected virtual void InitializeDbContext()
        {
            _context = DbContext;
        }
        
        protected virtual void CleanTests()
        {
            var db = new AlienzApiContext();

            if (PlayerPowerupUsagesIdsToCleanup.Any())
            {
                var usages = db.PlayerPowerupUsages.Where(l => PlayerPowerupUsagesIdsToCleanup.Contains(l.Id));

                foreach (var usage in usages)
                {
                    db.PlayerPowerupUsages.Remove(usage);
                }

                db.SaveChanges();
            }

            if (EnergyPurchaseIdsToCleanup.Any())
            {
                var purchases = db.EnergyPurchases.Where(l => EnergyPurchaseIdsToCleanup.Contains(l.Id));

                foreach (var purchase in purchases)
                {
                    db.EnergyPurchases.Remove(purchase);
                }

                db.SaveChanges();
            }

            if (PlayerDeathIdsToCleanup.Any())
            {
                var playerDeaths = db.PlayerDeaths.Where(l => PlayerDeathIdsToCleanup.Contains(l.Id));

                foreach (var death in playerDeaths)
                {
                    db.PlayerDeaths.Remove(death);
                }

                db.SaveChanges();
            }

            if (LevelAttemptIdsToCleanup.Any())
            {
                var levelAttempts = db.LevelAttempts.Where(l => LevelAttemptIdsToCleanup.Contains(l.Id));

                foreach (var attempt in levelAttempts)
                {
                    db.LevelAttempts.Remove(attempt);
                }

                db.SaveChanges();
            }

            if (TierScoreRewardIdsToCleanup.Any())
            {
                var tierScoreRewards = db.TierScoreRewards.Where(l => TierScoreRewardIdsToCleanup.Contains(l.Id));

                foreach (var tierScoreReward in tierScoreRewards)
                {
                    db.TierScoreRewards.Remove(tierScoreReward);
                }

                db.SaveChanges();
            }

            if (AwardReasonIdsToCleanup.Any())
            {
                var awardReasons = db.AwardReasons.Where(l => AwardReasonIdsToCleanup.Contains(l.Id));

                foreach (var awardReason in awardReasons)
                {
                    db.AwardReasons.Remove(awardReason);
                }

                db.SaveChanges();
            }

            if (LevelIdsToCleanup.Any())
            {
                var levels = db.Levels.Where(l => LevelIdsToCleanup.Contains(l.Id));

                foreach (var level in levels)
                {
                    db.Levels.Remove(level);
                }

                db.SaveChanges();
            }

            if (PlayerIdsToCleanup.Any())
            {
                var players = db.Players.Where(l => PlayerIdsToCleanup.Contains(l.Id));

                foreach (var player in players)
                {
                    db.Players.Remove(player);
                }

                db.SaveChanges();
            }

            ResetLists();
        }

        protected void ResetLists()
        {
            LevelAttemptIdsToCleanup = new List<int>();
            LevelIdsToCleanup = new List<int>();
            PlayerIdsToCleanup = new List<int>();
            AwardReasonIdsToCleanup = new List<int>();
            TierScoreRewardIdsToCleanup = new List<int>();
            PlayerDeathIdsToCleanup = new List<int>();
            EnergyPurchaseIdsToCleanup = new List<int>();
            PlayerPowerupUsagesIdsToCleanup = new List<int>();
        }

        protected Level GetTestLevel(int testWorld = 9999, int sequenceInWorld = 1, int startingFuel = 100, int startingTime = 500, bool active = true, bool isBlockingLevel = false)
        {
            Level level = _context.Levels.Add(new Level
            {
                World = testWorld,
                SequenceInWorld = sequenceInWorld,
                StartingFuel = startingFuel,
                StartingTime = startingTime,
                Active = active,
                IsBlockingLevel = isBlockingLevel
            });

            _context.SaveChanges();
            LevelIdsToCleanup.Add(level.Id);

            return level;
        }

        protected TierScoreReward GetTestTierScoreReward(int levelId, int awardReasonId, int tierNumber = 1, long score = 100)
        {
            TierScoreReward reward = _context.TierScoreRewards.Add(new TierScoreReward
            {
                TierNumber = tierNumber,
                Score = score,
                LevelId = levelId,
                AwardReasonId = awardReasonId
            });

            _context.SaveChanges();
            TierScoreRewardIdsToCleanup.Add(reward.Id);

            return reward;
        }

        protected LevelAttempt GetTestLevelAttempt(int playerId, int levelId, DateTime? date = null, int timesDied = 0, long score = 100, int timeSeconds = 0, bool completed = false)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }

            LevelAttempt attempt = _context.LevelAttempts.Add(new LevelAttempt
            {
                PlayerId = playerId,
                LevelId = levelId,
                Completed = completed,
                Date = date.Value,
                TimesDied = timesDied,
                Score = score,
                TimeSeconds = timeSeconds
            });

            _context.SaveChanges();
            LevelAttemptIdsToCleanup.Add(attempt.Id);

            return attempt;
        }

        protected Player GetTestPlayer()
        {
            Player player = _context.Players.Add(new Player());
            _context.SaveChanges();

            PlayerIdsToCleanup.Add(player.Id);

            return player;
        }

        protected AwardReason GetTestAwardReason(string name = "TestingReason", int energyAmount = 1, bool active = true)
        {
            AwardReason reason = _context.AwardReasons.Add(new AwardReason
            {
                Name = name,
                Active = active,
                EnergyRewardAmount = energyAmount
            });

            _context.SaveChanges();
            AwardReasonIdsToCleanup.Add(reason.Id);

            return reason;
        }

        protected PlayerDeath GetTestPlayerDeath(int playerId, int levelAttemptId, DateTime? irrelevantTime = null)
        {
            if (irrelevantTime == null)
            {
                irrelevantTime = DateTime.Now.AddMinutes(30);
            }

            PlayerDeath death = _context.PlayerDeaths.Add(new PlayerDeath
            {
                IrreleventTime = irrelevantTime.Value,
                LevelAttemptId = levelAttemptId,
                PlayerId = playerId
            });

            _context.SaveChanges();
            PlayerDeathIdsToCleanup.Add(death.Id);

            return death;
        }

        protected EnergyPurchase GetTestEnergyPurchase(int playerId, int energyPurchaseableItemId, DateTime? date = null)
        {
            if (date == null)
            {
                date = DateTime.Now.AddMinutes(30);
            }

            EnergyPurchase purchase = _context.EnergyPurchases.Add(new EnergyPurchase
            {
                Date = date.Value,
                EnergyPurchaseableItemId = energyPurchaseableItemId,
                PlayerId = playerId
            });

            _context.SaveChanges();
            EnergyPurchaseIdsToCleanup.Add(purchase.Id);

            return purchase;
        }

        protected PlayerPowerupUsage GetTestPlayerPowerupUsage(int playerId, int powerupId, int levelAttemptId,
            DateTime? date = null)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }

            PlayerPowerupUsage usage = _context.PlayerPowerupUsages.Add(new PlayerPowerupUsage
            {
                Date = date.Value,
                LevelAttemptId = levelAttemptId,
                PlayerId = playerId,
                PowerupId = powerupId
            });

            _context.SaveChanges();
            PlayerPowerupUsagesIdsToCleanup.Add(usage.Id);

            return usage;
        }

        protected List<TierScoreReward> Setup3TiersForLevel(int levelId)
        {
            List<TierScoreReward> tiers = new List<TierScoreReward>();
            for (int i = 0; i < 3; i++)
            {
                AwardReason reason = GetTestAwardReason($"Tier {i + 1}", i + 1);
                tiers.Add(GetTestTierScoreReward(levelId, reason.Id, i + 1));
            }

            return tiers;
        } 
    }
}
