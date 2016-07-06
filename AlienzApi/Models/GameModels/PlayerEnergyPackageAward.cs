using System;
using System.ComponentModel.DataAnnotations;

namespace AlienzApi.Models.GameModels
{
    public class PlayerEnergyPackageAward
    {
        public int Id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int AwardReasonId { get; set; }

        #region Navigation Properties
        public virtual Player Player { get; set; }
        public virtual AwardReason AwardReason { get; set; }
        #endregion
    }
}