using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlienzApi.Models.GameModels
{
    public class TierScoreReward
    {
        public int Id { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int TierNumber { get; set; }
        [Required, Range(0, long.MaxValue)]
        public long Score { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int AwardReasonId { get; set; }

        #region Navigation Properties
        public virtual Level Level { get; set; }
        public virtual AwardReason AwardReason { get; set; }
        #endregion
    }
}