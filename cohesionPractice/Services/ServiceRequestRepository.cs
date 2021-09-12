using cohesionPractice.Contexts;
using cohesionPractice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionPractice.Services
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly ServiceRequestContext _context;
        public ServiceRequestRepository(ServiceRequestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddServiceRequest(ServiceRequest serviceRequest)
        {
            var request = _context.ServiceRequests.Add(serviceRequest);
           
        }
        public void UpdatePointOfInterestForCity(Guid Id, ServiceRequest serviceRequest)
        {

        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public ServiceRequest GetServiceRequest(Guid Id)
        {


            return _context.ServiceRequests.Where(r => r.Id == Id).FirstOrDefault();
        }
        public bool ServiceRequestExists(Guid Id)
        {
            return _context.ServiceRequests.Any(r => r.Id == Id);
        }
        public IEnumerable<ServiceRequest> GetServiceRequests()
        {
            return _context.ServiceRequests.OrderBy(r => r.Id).ToList();
        }

        public void DeleteServiceRequest(ServiceRequest serviceRequest)
        {
            _context.ServiceRequests.Remove(serviceRequest);

        }
    }
}
