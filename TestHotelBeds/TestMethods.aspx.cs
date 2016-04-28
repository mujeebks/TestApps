using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;

namespace TestHotelBeds
{
    public partial class TestMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Bed Bank
            const string apiKey = "gfhpht2ffsfejd88g7pcnexe";
            const string sharedSecret = "e7Af9xbEtm";

            const string endpoint = "https://api.test.hotelbeds.com/hotel-api/1.0/status";

            // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
            string signature;
            using (var sha = SHA256.Create())
            {
                long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                //Console.WriteLine("Timestamp: " + ts);
                var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + sharedSecret + ts));
                signature = BitConverter.ToString(computedHash).Replace("-", "");
            }

            Console.WriteLine("Signature: " + signature);

            using (var client = new HttpClient())
            {
                // Request configuration 
                client.BaseAddress = new Uri("https://api.test.hotelbeds.com/hotel-content-api/1.0/hotels?fields=all");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                client.DefaultRequestHeaders.Add("X-Signature", signature);
                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
                //client.DefaultRequestHeaders.Add("Accept", "application/xml");
                //client.DefaultRequestHeaders.Add("SharedSecret", sharedSecret);
                // Request execution
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    var users = response.Content.ReadAsStringAsync();

                }
                #endregion

            }  
        }

        
    }
}