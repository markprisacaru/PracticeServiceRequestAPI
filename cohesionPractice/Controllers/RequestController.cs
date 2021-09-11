using cohesionPractice.Models;
using cohesionPractice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace cohesionPractice.Controllers
{


    [ApiController]
    [Route("api/servicerequest")]
    public class RequestController : ControllerBase
    {
        private readonly ILogger<RequestController> _logger;
        private FakeMailService _emailService;

        public RequestController(ILogger<RequestController> logger, FakeMailService emailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));

        }



        [HttpGet]
        public IActionResult GetRequests()
        {

            var requestsToReturn = ServiceRequestsDataStore.Current.ServiceRequests;
            //check if the return contains objects
            if (requestsToReturn.Count == 0 || requestsToReturn == null)
            {
                
                return NoContent();
            }

            return Ok(ServiceRequestsDataStore.Current.ServiceRequests);

            //return new JsonResult(ServiceRequestsDataStore.Current.ServiceRequests);

        }

        [HttpGet("{id}", Name ="GetServiceRequest")]
        public IActionResult GetRequest(Guid Id)
        {
            try
            {
                //throw new Exception("Example Exception");
               var requestToReturn = ServiceRequestsDataStore.Current.ServiceRequests.FirstOrDefault(r => r.Id == Id);
                            // check if the object exists
                            if (requestToReturn == null)
                            {
                                _logger.LogInformation($"Service request {Id} wasnt found when accessing the database");
                                return NotFound();
                            }

                            return Ok(requestToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting Service request with {Id} has occured", ex);
                return StatusCode(500, "a problem has happened while handling your request.");
            }

            

        }

        [HttpPost]
        public IActionResult CreateServiceRequest([FromBody] ServiceRequestCreation serviceRequest)
        {
            //var requestsToCreate = ServiceRequestsDataStore.Current.ServiceRequests;
            //var maxServiceRequestId = ServiceRequestsDataStore.Current.ServiceRequests.Max(p => p.Id);


            var finalServiceRequest = new ServiceRequest()
            {
                //assign new id, id should never be assigned by people 
                Id = Guid.NewGuid(),
                BuildingCode = serviceRequest.BuildingCode,
                Description = serviceRequest.Description,
                CurrentStatus = (CurrentStatusDTO)1,
                CreatedBy = serviceRequest.CreatedBy,
                //logical flow that when you create the ticket is the date used
                CreatedDate = DateTime.Now,
                LastModifiedBy = serviceRequest.LastModifiedBy,
                LastModifiedDate = DateTime.Now
            };


            ServiceRequestsDataStore.Current.ServiceRequests.Add(finalServiceRequest);

            return CreatedAtRoute("GetServiceRequest", new { id = finalServiceRequest.Id }, finalServiceRequest);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateRequest(Guid Id, [FromBody] ServiceRequestUpdate serviceRequest)
        {
            if (!ModelState.IsValid){ return BadRequest(ModelState); }
            var requestToReturn = ServiceRequestsDataStore.Current.ServiceRequests.FirstOrDefault(r => r.Id == Id);
            // check if the object exists
            if (requestToReturn == null)
            {
                return NotFound();
            }
            requestToReturn.Description = serviceRequest.Description;
            requestToReturn.BuildingCode = serviceRequest.BuildingCode;
            requestToReturn.LastModifiedBy = serviceRequest.LastModifiedBy;
            requestToReturn.LastModifiedDate = DateTime.Now;
           
            //if no status code is returned then the old one is kept
            if (serviceRequest.CurrentStatus == 0)
            { 
                serviceRequest.CurrentStatus = requestToReturn.CurrentStatus;
                _logger.LogInformation($"There was no status code given for request with {Id}. Defaulting back to the previous status");
            }
            
            else { requestToReturn.CurrentStatus = serviceRequest.CurrentStatus; }
            
            requestToReturn.CurrentStatus = serviceRequest.CurrentStatus;


            if (serviceRequest.CurrentStatus == (CurrentStatusDTO)3 )
            {
                _emailService.Send("Serice Request has been completed", $"Service Request with Id of {requestToReturn.Id} has been completed by {requestToReturn.LastModifiedBy} on {requestToReturn.LastModifiedDate}");

                TwilioClient.Init("AC33962bf7ecad411fc660cd1efa991381", "fa29415f6e5e1fc344d24e9ebe326fd8");

                var message = MessageResource.Create(
                    body: $"Service Request with Id of {requestToReturn.Id} has been completed by {requestToReturn.LastModifiedBy} on {requestToReturn.LastModifiedDate}",
                    from: new Twilio.Types.PhoneNumber("+15128723368"),
                    to: new Twilio.Types.PhoneNumber("+13125938089")
                );
                

            }

                return Ok();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRequest(Guid Id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var requestToDelete = ServiceRequestsDataStore.Current.ServiceRequests.FirstOrDefault(r => r.Id == Id);
            // check if the object exists
            if (requestToDelete == null)
            {
                return NotFound();
            }

            ServiceRequestsDataStore.Current.ServiceRequests.Remove(requestToDelete);
            return Ok();
        }




    }
}

