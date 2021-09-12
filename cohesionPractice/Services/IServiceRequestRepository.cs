using cohesionPractice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace cohesionPractice.Services
{
    public interface IServiceRequestRepository
    {
        IEnumerable<ServiceRequest> GetServiceRequests();
        ServiceRequest GetServiceRequest(Guid Id);

        void AddServiceRequest(ServiceRequest serviceRequest);
        bool ServiceRequestExists(Guid Id);
        void UpdatePointOfInterestForCity(Guid Id, ServiceRequest serviceRequest);
        void DeleteServiceRequest(ServiceRequest serviceRequest);
        bool Save();
        

    }
}
