using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class PlayerPowerupUsage
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int PowerupId { get; set; }
        public int LevelAttemptId { get; set; }
        public DateTime Date { get; set; }

        #region Navigation Properties
        public virtual Player Player { get; set; }
        public virtual Powerup Powerup { get; set; }
        public virtual LevelAttempt LevelAttempt { get; set; }
        #endregion
    }
}