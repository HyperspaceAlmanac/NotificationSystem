using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NotificationSystem
{
    public class TwilioSend
    {
        public void SendNotifications(List<String> numbers, string message)
        {
            // API calls should ideally be async. Did not find it in Twilio API on quick glance
            string accountSid = Secrets.TWILIO_SID;
            string authToken = Secrets.TWILIO_API_KEY;
            TwilioClient.Init(accountSid, authToken);
            foreach (String number in numbers)
            {
                string temp = number;
                /**
                MessageResource.Create(
                    body: message,
                    from: new Twilio.Types.PhoneNumber(Secrets.TWILIO_NUMBER),
                    to: new Twilio.Types.PhoneNumber(number));
                **/
            }
        }
    }

}
