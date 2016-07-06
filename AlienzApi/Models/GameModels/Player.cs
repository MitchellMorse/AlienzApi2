using System.Collections.Generic;
using AlienzApi.Models.ExampleModels;

namespace AlienzApi.Models.GameModels
{
    public class Player
    {
        public int Id { get; set; }

        #region Navigation Properties
        public virtual ICollection<PlayerPowerupUsage> PlayerPowerupUsages { get; set; }
        public virtual ICollection<EnergyPurchase>  EnergyPurchases { get; set; }
        public virtual ICollection<LevelAttempt> LevelAttempts { get; set; }
        public virtual ICollection<PlayerEnergyPackageAward> PlayerEnergyPackageAwards { get; set; }
        public virtual ICollection<PlayerEnergyPackagePurchase> PlayerEnergyPackagePurchases { get; set; }
        public virtual ICollection<AdView> AdViews { get; set; }
        #endregion
    }
}