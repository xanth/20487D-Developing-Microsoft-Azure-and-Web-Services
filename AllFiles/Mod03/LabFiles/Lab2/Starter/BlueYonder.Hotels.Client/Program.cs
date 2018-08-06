using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlueYonder.Hotels.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
              using (HttpClient client = new HttpClient())
            {
                string baseUrl = "http://localhost:5000/api/HotelBooking";

                // POST request                
                HttpResponseMessage response = await client.PostAsync(baseUrl, new StringContent("Hotel3"));
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                
                // PUT request
                response = await client.PutAsync(baseUrl, new StringContent("Hotel3"));
                content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
    }
}
