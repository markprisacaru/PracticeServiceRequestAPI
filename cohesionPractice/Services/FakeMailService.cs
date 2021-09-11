using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace cohesionPractice.Services
{
    public class FakeMailService
    {
        private string _mailTo = "admin@cohesion.com";
        private string _mailFrom = "noreply@cohesion.com";

        public void Send(string subject, string message)
        {
            //send mail(outout to debug since i dont have a mail service
            Debug.WriteLine($"Mail from {_mailFrom} to  {_mailTo}, with fakemailservice ");
            Debug.WriteLine($"Subject {subject}");
            Debug.WriteLine($"Subject {message}");

        }

    }


}
