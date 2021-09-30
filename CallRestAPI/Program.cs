using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace CallRestAPI
{
    class Program
    {
        private const string URL = "http://localhost:5555";

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            //Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //List data response
            // Blocking call! Program will wait here until a response is received or a timeout occurs.
            HttpResponseMessage response = client.GetAsync("api/pizza").Result;
            //api/pizza to get all
            //api/pizza/1 to get pizza with id 1

            if (response.IsSuccessStatusCode)
            {
                //Parse the response body
                //Single
                //Pizza pizza = response.Content.ReadAsAsync<Pizza>().Result;
                //Console.WriteLine("{0}", pizza.Name);
                //Multiple
                var pizzas = response.Content.ReadAsAsync<IEnumerable<Pizza>>().Result;

                foreach (var pizza in pizzas)
                {
                    Console.WriteLine("{0}", pizza.Name);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            client.Dispose();
        }
    }

    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}