using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IdentityClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            var postRes = await client.PostAsync("https://localhost:5001/api/user/register", new StringContent("{ \"Username\": \"avigi@dima.com\", \"Password\": \"AsdAsd123123!!!\" }", Encoding.UTF8, "application/json"));
            var token = await postRes.Content.ReadAsStringAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await client.GetAsync("https://localhost:5001/api/values");
            var str = await res.Content.ReadAsStringAsync();
            Console.WriteLine(str);
        }
    }
}
