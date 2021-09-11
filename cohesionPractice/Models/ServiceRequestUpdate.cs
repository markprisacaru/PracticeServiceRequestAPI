using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionPractice.Models
{
    public class ServiceRequestUpdate
    {
        [Required(ErrorMessage = "you should provide a building code")]
        public string BuildingCode { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        
        [Required(ErrorMessage = "you should provide a Current Status"), EnumDataType(typeof(CurrentStatusDTO))]
        public CurrentStatusDTO CurrentStatus { get; set; }
        
        [Required(ErrorMessage = "you should provide a user")]
        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }


    }
}
