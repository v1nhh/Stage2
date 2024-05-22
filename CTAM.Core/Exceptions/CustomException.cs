using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CTAM.Core.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public new string Message { get; set; }
        public Dictionary<string,string> Parameters { get; set; }

        public CustomException(HttpStatusCode statusCode, string message, Dictionary<string,string> parameters = null) : base(message + ((parameters != null) ? ":" + JsonConvert.SerializeObject(parameters) : ""))
        {
            StatusCode = statusCode;
            Message = message;
            Parameters = parameters ?? new Dictionary<string, string>();
        }

        public ObjectResult GetObjectResult(ControllerBase controller)
        {
            return controller.StatusCode((int)StatusCode, new
            {
                Message = Parameters.Aggregate(Message, (current, replacement) => current.Replace($"{{{replacement.Key}}}", replacement.Value))
            });
        }
    }
}
