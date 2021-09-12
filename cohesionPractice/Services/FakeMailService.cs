using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionPractice.Services
{
    public class FakeMailService : IFakeMailService
    {
        private readonly IConfiguration _configuration;

        
        public FakeMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentOutOfRangeException(nameof(configuration));
        }


        public void Send(string subject, string message)
        {
            //send mail(outout to debug since i dont have a mail service
            Debug.WriteLine($"Mail from {_configuration["mailSettings:mailFromAddress"]} to  {_configuration["mailSettings:mailToAddress"]}, with fakemailservice ");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Subject {message}");

        }

    }


}
