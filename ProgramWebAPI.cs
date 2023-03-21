using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleWebAPI
{

    static public void Main()
    {

        Console.WriteLine("Main Method");
    }
    public class Class1
    {
        private const string URL = @"http://127.0.0.1/login";
        private static object urlParameters = new { username = "admin", password = "123" };

        public static async void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(urlParameters);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            // List data response.
            HttpResponseMessage response = await client.PostAsync(URL, data);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}