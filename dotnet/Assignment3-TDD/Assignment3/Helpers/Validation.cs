using System;
using System.Linq;
using System.Net.Cache;

namespace RDJTPServer.Helpers
{
    public class Request
    {
        public string Method;
    }
    public class Response
    {
        public string Status { get; set; }
    }

    public class Validation
    {
        public Response ValidateRequest(Request request)
        {
            var response = new Response();
            var allowedMethods = new[] { "create", "read", "update", "delete" };
            if (request.Method == null)
            {
                response.Status = "Missing method in header.";
            }

            if (!allowedMethods.Contains(request.Method))
            {
                Console.WriteLine(request.Method);
                response.Status = "Illegal method provided.";
            }
            return response;
        }

    }
}