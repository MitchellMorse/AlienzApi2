using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class BlockWall
    {
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int World { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Sequence { get; set; }

        #region Navigation Properties
        public virtual ICollection<EnergyPurchaseableItem> EnergyPurchaseableItems { get; set; } 
        #endregion
    }
}