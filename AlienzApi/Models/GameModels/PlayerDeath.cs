using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class PlayerDeath
    {
        public int Id { get; set; }
        public DateTime IrreleventTime { get; set; }
        public int LevelAttemptId { get; set; }
        public int PlayerId { get; set; }

        #region Navigation Properties
        public virtual Player Player { get; set; }
        public virtual LevelAttempt LevelAttempt { get; set; }
        #endregion
    }
}