﻿using System.ComponentModel.DataAnnotations;

namespace CityInfo.api.Models
{
    public class PointOfIntrestCreationDto
    {
        [Required (ErrorMessage = "You shuld provide a name")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
 