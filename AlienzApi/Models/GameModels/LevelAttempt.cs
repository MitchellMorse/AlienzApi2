using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlienzApi.Models.GameModels
{
    public class LevelAttempt
    {
        public int Id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required, Range(0, Int32.MaxValue)]
        public int TimesDied { get; set; }
        [Required, Range(0, long.MaxValue)]
        public long Score { get; set; }
        [Required, Range(0, Int32.MaxValue)]
        public int TimeSeconds { get; set; }

        #region Navigation Properties
        public virtual Player Player { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<PlayerPowerupUsage> PlayerPowerupUsages { get; set; }
        #endregion
    }
}