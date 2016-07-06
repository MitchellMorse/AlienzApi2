using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.DTO
{
    public class LevelDto
    {
        public int World { get; set; }
        public int Sequence { get; set; }
        public int StartingFuel { get; set; }
        public int StartingTimeSeconds { get; set; }
        public int PlayerHighScore { get; set; }
        public int Tier1Score { get; set; }
        public int Tier2Score { get; set; }
        public int Tier3Score { get; set; }
        public int Tier1Reward { get; set; }
        public int Tier2Reward { get; set; }
        public int Tier3Reward { get; set; }
    }
}