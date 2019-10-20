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
        public List<string> Status { get; set; }
        public string Body { get; set; }
    }

    static class StatusCodes
    {
        public const string Ok = "1 Ok";
        public const string Created = "2 Created";
        public const string Updated = "3 Updated";
        public const string BadRequest = "4 Bad Request";
        public const string NotFound = "5 Not Found";
        public const string Error = "6 Error";

    }

    //public enum StatusCode
    //{
    //    Ok = 1,
    //    Created = 2,
    //    Updated = 3,
    //    BadRequest = 4,
    //    NotFound = 5,
    //    Error = 6
    //}

    public class Validation
    {

        public Response ValidateRequest(Request request)
        {

            var response = new Response();
            string errorMessage;
            var allowedMethods = new[] { "create", "read", "update", "delete", "echo", "testing" };

            if (request.Method == null)
            {
                errorMessage = "Missing method in header.";
                response.Status.Add($"{StatusCodes.BadRequest}: {string.Join("\n ", errorMessage)}");
                //response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            }

            if (!allowedMethods.Contains(request.Method))
            {
                errorMessage = "Illegal method request provided.";
                response.Status.Add($"{StatusCodes.BadRequest}: {string.Join("\n ", errorMessage)}");
            }

            //if (!allowedMethods.Contains(request.Path))
            //{
            //    errorResponse.Add("Missing resource in request header.");
            //    response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            //}

            //if (request.Date == null)
            //{
            //    errorResponse.Add("Missing date in request header.");
            //    response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            //}

            //// TODO: Research the following!!
            //if (request.Date != null && request.Date != Utilities.UnixTimestamp())
            //{
            //    errorResponse.Add("Illegal date in request header.");
            //    response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            //}

            //if (request.Body == null)
            //{
            //    errorResponse.Add("Missing body in header.");
            //    response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            //}

            //if (request.Body == null)
            //{
            //    errorResponse.Add("Missing body in header.");
            //    response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            //}

            //if (request.Body != null && !Utilities.IsValidJson(request.Body))
            //{
            //    errorResponse.Add("Illegal body.");
            //    response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            //}

            //// TODO: Fix the test!
            //if (request.Method != null && request.Method.Contains("echo"))
            //{
            //    Console.WriteLine(request.Body);
            //    response.Body = request.Body;
            //}

            //if (request.Path != null)
            //{
            //    var pathSplit = request.Path.Split("/");
            //    if (pathSplit.Length > 1 && pathSplit[2] != "categories")
            //    {
            //        errorResponse.Add("Illegal body.");
            //        response.Status = $"{StatusCodes.BadRequest}: {string.Join(", ", errorResponse)}";
            //    }
            //}

            Console.WriteLine(response.Status);

            return response;

        }

    }
}