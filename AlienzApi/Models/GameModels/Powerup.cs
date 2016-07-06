using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class Powerup
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Name { get; set; }

        #region Navigation Properties
        public virtual ICollection<PlayerPowerupUsage> PlayerPowerupUsages { get; set; } 
        #endregion
    }
}