using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;

namespace RDJTPServer.Helpers
{
    public class Request
    {
        public string Method;
        public string Path;
        public string Date;
        public string Body;
    }
    public class Response
    {
        public string Status { get; set; }
        public string Body { get; set; }
    }

    public enum StatusCode
    {
        Ok = 1,
        Created = 2,
        Updated = 3,
        BadRequest = 4,
        NotFound = 5,
        Error = 6
    }

    public class Validation
    {
        public Response ValidateRequest(Request request)
        {
            var response = new Response();
            var errorResponse = new List<string>();
            var allowedMethods = new[] { "create", "read", "update", "delete", "echo", "testing" };

            if (request.Method == null)
            {
                errorResponse.Add("Missing method in header.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            if (!allowedMethods.Contains(request.Method))
            {
                errorResponse.Add("Illegal method request provided.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            if (!allowedMethods.Contains(request.Path))
            {
                errorResponse.Add("Missing resource in request header.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            if (request.Date == null)
            {
                errorResponse.Add("Missing date in request header.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            // TODO: Research the following!!
            if (request.Date != null && request.Date != Utilities.UnixTimestamp())
            {
                errorResponse.Add("Illegal date in request header.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            if (request.Body == null)
            {
                errorResponse.Add("Missing body in header.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            if (request.Body == null)
            {
                errorResponse.Add("Missing body in header.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            if (!Utilities.IsValidJson(request.Body))
            {
                errorResponse.Add("Illegal body.");
                response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            // TODO: Fix the test!
            if (request.Method != null && request.Method.Contains("echo"))
            {
                Console.WriteLine(request.Body);
                response.Body = request.Body;
            }

            var pathEndpoint = request.Path.Split("/")[2];
            //if (pathEndpoint != "categories")
            //{
            //    errorResponse.Add("Illegal body.");
            //    response.Status = $"{StatusCode.BadRequest}: {string.Join(", ", errorResponse)}";
            //}

            return response;

        }

    }
}