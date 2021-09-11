using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace cohesionPractice.Models
{

    
    public class ServiceRequest
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(CurrentStatusDTO))]
        public CurrentStatusDTO CurrentStatus{ get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

    
    }
    
    public enum CurrentStatusDTO
    {
        NotApplicable = 0,
        Created = 1,
        InProgress = 2 ,
        Complete = 3,
        Canceled = 4 
    }
}
