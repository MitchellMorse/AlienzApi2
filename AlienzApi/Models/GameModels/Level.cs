using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlienzApi.Models.GameModels
{
    public class Level
    {
        public int Id { get; set; }

        [Required, Range(0, 9999)]
        public int World { get; set; }

        [Required, Range(0, 9999)]
        public int SequenceInWorld { get; set; }
        public int StartingFuel { get; set; }
        public int StartingTime { get; set; }
        public bool Active { get; set; }
        public bool IsBlockingLevel { get; set; }

        #region Navigation Properties
        public virtual ICollection<TierScoreReward> TierScoreRewards { get; set; } 
        public virtual ICollection<LevelAttempt> LevelAttempts { get; set; }
        #endregion
    }
}