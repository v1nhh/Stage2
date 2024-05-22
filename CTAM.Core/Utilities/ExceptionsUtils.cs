using CTAM.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace CTAM.Core.Utilities
{
    public static class ExceptionUtils
    {
        public static Exception GetMostInnerException(this Exception exception)
        {
            var mostInnerException = exception;
            while (mostInnerException.InnerException != null)
            {
                mostInnerException = mostInnerException.InnerException;
            }
            return mostInnerException;
        }

        public static ObjectResult GetObjectResult<ControllerType>(this Exception exception, ILogger<ControllerType> logger, ControllerBase controller, HttpStatusCode? statusCode = null)
        {
            logger.LogError(exception, exception.GetMostInnerException().Message);

            if(exception is CustomException ce)
            {
                return ce.GetObjectResult(controller);
            }

            return controller.StatusCode(statusCode != null ? (int)statusCode : (int)HttpStatusCode.BadRequest, new 
            {
                Message = exception.GetMostInnerException().Message
            });
        }
    }
}