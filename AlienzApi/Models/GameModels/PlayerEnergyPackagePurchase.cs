using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class PlayerEnergyPackagePurchase
    {
        public int Id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public int EnergyPackageId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        #region Navigation Properties
        public virtual Player Player { get; set; }
        public virtual EnergyPackage EnergyPackage { get; set; }
        #endregion
    }
}