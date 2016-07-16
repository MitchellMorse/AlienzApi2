using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.DTO
{
    public class GameStartDto
    {
        public ICollection<LevelDto> AllLevelsInCurrentWorld { get; set; }  
        public int CurrentLevelId { get; set; }
        public int PlayerLives { get; set; }
        public ICollection<KeyValuePair<string, int>> PowerupCount { get; set; }
    }
}