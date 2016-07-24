using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AlienzApi.Models.GameModels;

namespace AlienzApi.Models.Interfaces
{
    public interface IAlienzApiContext : IDisposable
    {
        DbSet<Level> Levels { get; }
        DbSet<Player> Players { get; }
        DbSet<LevelAttempt> LevelAttempts { get; }
        DbSet<TierScoreReward> TierScoreRewards { get; }
        DbSet<AwardReason> AwardReasons { get; }
        DbSet<PlayerDeath> PlayerDeaths { get; }
        DbSet<EnergyPurchaseableItem> EnergyPurchaseableItems { get; }
        DbSet<Powerup> Powerups { get; }
        DbSet<PlayerPowerupUsage> PlayerPowerupUsages { get; }
        DbSet<EnergyPurchase> EnergyPurchases { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void MarkAsModified<t>(t item) where t : class;
    }
}
