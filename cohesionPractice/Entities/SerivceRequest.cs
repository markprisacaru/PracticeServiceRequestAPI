using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionPractice.Entities
{
    public class ServiceRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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

    public enum CurrentStatusDTO
    {
        NotApplicable = 0,
        Created = 1,
        InProgress = 2,
        Complete = 3,
        Canceled = 4
    }
}
