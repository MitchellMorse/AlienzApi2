using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class Level
    {
        public int Id { get; set; }

        [Required, Range(0, 999)]
        public int World { get; set; }

        [Required, Range(0, 999)]
        public int SequenceInWorld { get; set; }

        public int Tier2Score { get; set; }
        public int Tier2Reward { get; set; }
        public int Tier3Score { get; set; }
        public int Tier3Reward { get; set; }
        public int Tier1Reward { get; set; }
        public int StartingFuel { get; set; }
        public int StartingTime { get; set; }
    }
}