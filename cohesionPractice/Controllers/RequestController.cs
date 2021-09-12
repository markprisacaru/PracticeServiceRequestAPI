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
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace cohesionPractice.Controllers
{


    [ApiController]
    [Route("api/servicerequest")]
    public class RequestController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<RequestController> _logger;
        private IFakeMailService _emailService;

        private readonly IServiceRequestRepository _serviceRequestRepository;
        public RequestController(ILogger<RequestController> logger, IFakeMailService emailService, IConfiguration iConfig, IServiceRequestRepository serviceRequestRepository, IMapper mapper)
        {
            _serviceRequestRepository = serviceRequestRepository ?? throw new ArgumentNullException(nameof(serviceRequestRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _config = iConfig;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


        }



        [HttpGet]
        public ActionResult GetRequests()
        {

            var requestsEntities = _serviceRequestRepository.GetServiceRequests();
            var requestsToReturn = _mapper.Map<IEnumerable<ServiceRequest>>(requestsEntities);
            //check if the return contains objects
            
            return Ok(requestsToReturn);
        }

        [HttpGet("{id}", Name ="GetServiceRequest")]
        public IActionResult GetRequest(Guid Id)
        {
            var serviceRequestEntity = _serviceRequestRepository.GetServiceRequest(Id);
               
            if (serviceRequestEntity == null)
            {
                _logger.LogInformation($"Service request {Id} wasnt found when accessing the database");
                return NotFound();
            }
           
            var requestToReturn = _mapper.Map<ServiceRequest>(serviceRequestEntity);

           
            return Ok(requestToReturn);
                //_logger.LogCritical($"Exception while getting Service request with {Id} has occured", ex);
                // StatusCode(500, "a problem has happened while handling your request.");
        }

                
        

        [HttpPost]
        public IActionResult CreateServiceRequest([FromBody] ServiceRequestCreation serviceRequest)
        {

            var manualMap = new Entities.ServiceRequest()
            {
                //assign new id, id should never be assigned by people 
                Id = Guid.NewGuid(),
                BuildingCode = serviceRequest.BuildingCode,
                Description = serviceRequest.Description,
                CurrentStatus = (Entities.CurrentStatusDTO)(CurrentStatusDTO)1,
                CreatedBy = serviceRequest.CreatedBy,
                //logical flow that when you create the ticket is the date used
                CreatedDate = DateTime.Now,
                LastModifiedBy = serviceRequest.LastModifiedBy,
                LastModifiedDate = DateTime.Now
            };

            var finalServiceRequest = //manualMap;
                                      _mapper.Map<Entities.ServiceRequest>(manualMap);




            _serviceRequestRepository.AddServiceRequest(finalServiceRequest);
            _serviceRequestRepository.Save();
            var creatredRequest = _mapper.Map<Models.ServiceRequest>(finalServiceRequest);

            return CreatedAtRoute("GetServiceRequest", new { id = creatredRequest.Id }, creatredRequest);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateRequest(Guid Id, [FromBody] ServiceRequestUpdate serviceRequest)
        {
            //pulll config info from appsettings.json
            string user = _config.GetSection("textAPIsettings").GetSection("username").Value;
            string sid = _config.GetSection("textAPIsettings").GetSection("sid").Value;

            if (!ModelState.IsValid){ return BadRequest(ModelState); }


            var requestToReturn = _serviceRequestRepository.GetServiceRequest(Id);
            // check if the object exists
            if (!_serviceRequestRepository.ServiceRequestExists(Id))
            {
                return NotFound();
            }

            var serviceRequestEntity = _serviceRequestRepository.GetServiceRequest(Id);
            


            requestToReturn.Description = serviceRequest.Description;
            requestToReturn.BuildingCode = serviceRequest.BuildingCode;
            requestToReturn.LastModifiedBy = serviceRequest.LastModifiedBy;
            requestToReturn.LastModifiedDate = DateTime.Now;
            serviceRequestEntity = _mapper.Map<Entities.ServiceRequest>(serviceRequest);
            _mapper.Map(requestToReturn, serviceRequestEntity);

            //if no status code is returned then the old one is kept
            if (serviceRequestEntity.CurrentStatus == 0)
            {
                serviceRequestEntity.CurrentStatus = requestToReturn.CurrentStatus;
                _logger.LogInformation($"There was no status code given for request with {Id}. Defaulting back to the previous status");
            }
            
            else { requestToReturn.CurrentStatus = serviceRequestEntity.CurrentStatus; }
            
            requestToReturn.CurrentStatus = serviceRequestEntity.CurrentStatus;


            if (serviceRequest.CurrentStatus == (CurrentStatusDTO)3 )
                        {
                            _emailService.Send("Service Request has been completed", $"Service Request with Id of {requestToReturn.Id} has been completed by {requestToReturn.LastModifiedBy} on {requestToReturn.LastModifiedDate}");

                        //text service disabled for dev since it costs money turn on during demo
                        /* 
                        TwilioClient.Init(user, sid);

                        var message = MessageResource.Create(
                            body: $"Service Request with Id of {requestToReturn.Id} has been completed by {requestToReturn.LastModifiedBy} on {requestToReturn.LastModifiedDate}",
                            from: new Twilio.Types.PhoneNumber("+15128723368"),
                            to: new Twilio.Types.PhoneNumber("+xxxxxxxxxxx")
                        );*/





            }
            _mapper.Map(requestToReturn, serviceRequestEntity);
            _serviceRequestRepository.UpdatePointOfInterestForCity(Id, serviceRequestEntity);

            _serviceRequestRepository.Save();

            return Ok();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRequest(Guid Id)
        {
            // check if the object exists
            if (!_serviceRequestRepository.ServiceRequestExists(Id))
            {
                return NotFound();
            }
            var serviceRequestEntity = _serviceRequestRepository.GetServiceRequest(Id);
            _serviceRequestRepository.DeleteServiceRequest(serviceRequestEntity);
            _serviceRequestRepository.Save();
            
            return Ok();
        }




    }
}

