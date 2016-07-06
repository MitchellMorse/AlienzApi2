using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlienzApi.Models.GameModels
{
    public class AdView
    {
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int PlayerId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}