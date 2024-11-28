using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroserviceProject.Shared
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        public Microsoft.AspNetCore.Mvc.ProblemDetails? Fail { get; set; }
        [JsonIgnore]
        public bool IsSuccess => Fail is null;
        [JsonIgnore]
        public bool MyProperty => !IsSuccess;

        public static ServiceResult SucccessAsNocontent()
        {
            return new ServiceResult { Status = HttpStatusCode.NoContent };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Title = "NotFound",
                    Detail = "The requested resource was not found."
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult
                {
                    Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(exception.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });

            return new ServiceResult
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }

        public static ServiceResult Error(Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = problemDetails
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                }
            };
        }

        public static ServiceResult Error(string title, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                }
            };
        }

        public static ServiceResult ErrorFromValidation(IDictionary<string, object> errors)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                {
                    Title = "Validaton errors occured.",
                    Detail = "Please check the errors property for more details.",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }

    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SucccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,
                Data = data
            };
        }

        public static ServiceResult<T> SucccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }



        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>
                {
                    Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }
            
            var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(exception.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });

            return new ServiceResult<T>
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }
               
        public new static ServiceResult<T> Error(Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = problemDetails
            };
        }
               
        public new static ServiceResult<T> Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                }
            };
        }
               
        public new static ServiceResult<T> Error(string title, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                }
            };
        }
               
        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string,object> errors)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                {
                    Title = "Validaton errors occured.",
                    Detail = "Please check the errors property for more details.",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }
    }
}
