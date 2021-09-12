using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cohesionPractice.Models;

namespace cohesionPractice.Profiles
{
    public class ServiceRequestProfile :Profile
    {
        public ServiceRequestProfile()
        {
            CreateMap<Entities.ServiceRequest, Models.ServiceRequest>();
            CreateMap<Entities.ServiceRequest, Models.ServiceRequestCreation>();
            CreateMap<Entities.ServiceRequest, Models.ServiceRequestUpdate>();
            CreateMap<Models.ServiceRequestCreation, Entities.ServiceRequest>();
            CreateMap<Models.ServiceRequestUpdate, Entities.ServiceRequest>();



        }
    }
}
