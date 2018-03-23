using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "admin:123");
            var result = client.GetAsync(new Uri("http://localhost:19878/api/brand/GetBrands")).Result;
            if (result != null)
            {
                Console.WriteLine("Done" + result.StatusCode);
            }
            else
                Console.WriteLine("Error" + result.StatusCode);
            Console.ReadLine();  
        }
    }
}
