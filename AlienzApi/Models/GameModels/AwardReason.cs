
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlienzApi.Models.GameModels
{
    public class AwardReason
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int EnergyRewardAmount { get; set; }
        [Required]
        public bool Active { get; set; }

        #region Navigation Properties
        public virtual List<TierScoreReward> TierScoreRewards { get; set; }
        #endregion
    }
}