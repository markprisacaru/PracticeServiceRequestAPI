using cohesionPractice.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace cohesionPractice.Entities
{
    public class ServiceRequestCreationEntity
    {


            [Required(ErrorMessage = "you should provide a building code")]
            public string BuildingCode { get; set; }
            [MaxLength(500)]
            [Required(ErrorMessage = "you should provide a Description")]
            public string Description { get; set; }
            [EnumDataType(typeof(CurrentStatusDTO))]
            public CurrentStatusDTO CurrentStatus { get; set; }
            [Required(ErrorMessage = "you should provide a user")]
            public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            [Required(ErrorMessage = "you should provide a user")]
            public string LastModifiedBy { get; set; }
            public DateTime LastModifiedDate { get; set; }





        
    }
}