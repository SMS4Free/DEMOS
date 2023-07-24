using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace sms4free_client_demo
{
    internal class Program
    {
        public class Response
        {
            public string status { get; set; }
            public string message { get; set; }
        }
        static async Task Main(string[] args)
        {
            using (HttpClient ApiClient = new HttpClient())
            {
               

                string key = "YOUR_API_KEY";
                string user = "YOUR_PHONE_NUMBER";
                string pass = "YOUR_PASSWORD";
                string sender = "SENDER";
                string recipient = "0512345678; 0512345678"; // Numbers must be separated with ;
                string msg = "SMS4FREE"; // can be anything

                var requestObject = new
                {
                    key,
                    user,
                    pass,
                    sender,
                    recipient,
                    msg
                };

                var dataAsJson = JsonConvert.SerializeObject(requestObject);
                var buffer = Encoding.UTF8.GetBytes(dataAsJson);

                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await ApiClient.PostAsync("https://api.sms4free.co.il/v2/SendSMS", byteContent);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<Response>(responseString);
                if (response.IsSuccessStatusCode)
                {
                   

                    Console.WriteLine("Status: " + jsonResponse.status + "\nMessage: " + jsonResponse.message);

                }
                else
                {
                    Console.WriteLine("Failed to send SMS. Status Code: " + response.StatusCode);
                    Console.WriteLine("Status: " + jsonResponse.status + "\nMessage: " + jsonResponse.message);
                }
            }
         
    }
    }
}

