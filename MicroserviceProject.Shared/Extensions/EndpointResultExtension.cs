﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceProject.Shared.Extensions
{
    public static class EndpointResultExtension
    {
        public static IResult ToGenericResult<T>(this ServiceResult<T> result)
        {
            return result.Status switch
            {
                System.Net.HttpStatusCode.OK => Results.Ok(result),
                System.Net.HttpStatusCode.Created => Results.Created(result.UrlAsCreated, result),
                System.Net.HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!)
            };
        }

        public static IResult ToGenericResult(this ServiceResult result)
        {
            return result.Status switch
            {
                
                System.Net.HttpStatusCode.NoContent => Results.NoContent(),
                System.Net.HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!)
            };
        }
    }
}
