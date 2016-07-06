using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class EnergyPurchaseableItem
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Name { get; set; }
        public int? PowerupId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int EnergyCost { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public bool Active { get; set; }
        public int? BlockWallId { get; set; }

        #region Navigation Properties
        public virtual ICollection<Powerup> Powerups { get; set; } 
        public virtual ICollection<BlockWall> BlockWalls { get; set; } 
        #endregion
    }
}