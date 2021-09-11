using cohesionPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace cohesionPractice
{
    public class ServiceRequestsDataStore
    {
        public static ServiceRequestsDataStore Current { get;  } = new ServiceRequestsDataStore();
        public List<ServiceRequest> ServiceRequests { get; set; }

        public ServiceRequestsDataStore() => ServiceRequests = new List<ServiceRequest>()
            {

                new ServiceRequest()
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                    BuildingCode = "COH",
                    Description = "Please turn up the AC in suite 1200D. It is too hot here.",
                    CurrentStatus = (CurrentStatusDTO)1,
                    CreatedBy = "Nik Patel",
                    CreatedDate = new DateTime(2019,08,01,14,01,23,1),
                    LastModifiedBy = "Jane Doe",
                    LastModifiedDate = new DateTime(2019,09,01,14,01,23,1)
                }
                ,
                new ServiceRequest()
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea5"),
                    BuildingCode = "ACH",
                    Description = "Take me home country roads cause It is freezing cold in here! So turn down the AC!",
                    CurrentStatus = (CurrentStatusDTO)3,
                    CreatedBy = "John Denver",
                    CreatedDate = new DateTime(2020,09,01,14,01,23,122),
                    LastModifiedBy = "Jane Tech support",
                    LastModifiedDate = new DateTime(2020,12,01,14,01,23,1)
                }


            };






    }
}
