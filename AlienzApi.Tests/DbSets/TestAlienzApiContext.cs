using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Tests.DbSets
{
    public class TestAlienzApiContext : IAlienzApiContext
    {
        public DbSet<Level> Levels { get;set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<LevelAttempt> LevelAttempts { get; set; }
        public DbSet<TierScoreReward> TierScoreRewards { get; set; }
        public DbSet<AwardReason> AwardReasons { get; set; }
        public DbSet<PlayerDeath> PlayerDeaths { get; set; }
        public DbSet<PlayerPowerupUsage> PlayerPowerupUsages { get; set; }
        public DbSet<EnergyPurchaseableItem> EnergyPurchaseableItems { get; set; }
        public DbSet<Powerup> Powerups { get; set; }
        public DbSet<EnergyPurchase> EnergyPurchases { get; set; }

        public void Dispose()
        {
            
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        public void MarkAsModified<t>(t item) where t : class
        {
            
        }

        public TestAlienzApiContext()
        {
            this.Levels = new TestLevelDbSet();
            this.Players = new TestPlayerDbSet();
            this.LevelAttempts = new TestLevelAttemptDbSet();
            this.TierScoreRewards = new TestTierScoreRewardDbSet();
        }
    }
}
