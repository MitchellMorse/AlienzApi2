using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AlienzApi.Models.GameModels;
using AlienzApi.Models.Interfaces;

namespace AlienzApi.Models
{
    public class AlienzApiContext : DbContext, IAlienzApiContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AlienzApiContext() : base("name=AlienzApiContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<AlienzApi.Models.ExampleModels.Author> Authors { get; set; }

        public System.Data.Entity.DbSet<AlienzApi.Models.ExampleModels.Book> Books { get; set; }

        public DbSet<Level> Levels { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TierScoreReward> TierScoreRewards { get; set; }
        public DbSet<EnergyPurchase> EnergyPurchases { get; set; }
        public DbSet<LevelAttempt> LevelAttempts { get; set; }
        public DbSet<PlayerEnergyPackageAward> PlayerEnergyPackageAwards { get; set; }
        public DbSet<PlayerEnergyPackagePurchase> PlayerEnergyPackagePurchases { get; set; }
        public DbSet<AwardReason> AwardReasons { get; set; }
        public DbSet<EnergyPackage> EnergyPackages { get; set; }
        public DbSet<PlayerPowerupUsage> PlayerPowerupUsages { get; set; }
        public DbSet<EnergyPurchaseableItem> EnergyPurchaseableItems { get; set; }
        public DbSet<BlockWall> BlockWalls { get; set; }
        public DbSet<AdView> AdViews { get; set; }
        public DbSet<Powerup> Powerups { get; set; }

        public void MarkAsModified(Level item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}
