using System;
using System.ComponentModel.DataAnnotations;

namespace AlienzApi.Models.GameModels
{
    public class EnergyPurchase
    {
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int PlayerId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int EnergyPurchaseableItemId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        #region Navigation Properties
        public virtual Player Player { get; set; }
        public virtual EnergyPurchaseableItem EnergyPurchaseableItem { get; set; }
        #endregion
    }
}