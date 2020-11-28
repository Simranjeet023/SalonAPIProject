using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalonAPIClient.Helper
{
    public class ServerApi
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://tchaw-eval-prod.apigee.net/salonapiproxy/api/");
            return client;
        }   //  HttpClient object End
    }   //  classEnd
}
