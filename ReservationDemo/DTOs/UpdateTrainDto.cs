using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class UpdateTrainDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string TrainName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string TrainType { get; set; } = string.Empty;

        [Required]
        public int StartStationId { get; set; }

        [Required]
        public int EndStationId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Base fare must be non-negative.")]
        public decimal BaseFare { get; set; }
    }
}