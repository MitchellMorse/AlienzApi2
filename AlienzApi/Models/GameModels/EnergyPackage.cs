using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlienzApi.Models.GameModels
{
    public class EnergyPackage
    {
        public int Id { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public int Amount { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required, Range(0, float.MaxValue)]
        public float DollarCost { get; set; }

        #region Navigation Properties
        public virtual ICollection<PlayerEnergyPackagePurchase> PlayerEnergyPackagePurchases { get; set; } 
        #endregion
    }
}