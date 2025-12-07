using MediatR;
using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace MicroserviceProject.Shared
{
    public interface IRequestByServiceResult<T> : IRequest<ServiceResult<T>>;
    public interface IRequestByServiceResult : IRequest<ServiceResult>;

    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Fail is null;

        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Resource not found",
                    Detail = "The requested resource could not be found"
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            return new ServiceResult()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }

        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Fail = problemDetails,
                Status = status
            };
        }

        public static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                },
                Status = status
            };
        }

        public static ServiceResult Error(string title, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                },
                Status = status
            };
        }

        public static ServiceResult ErrorFromValdation(IDictionary<string, object> errors)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured.",
                    Detail = "See the errors property for details.",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors
                },
                Status = HttpStatusCode.BadRequest
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,
                Data = data
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }

        public static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                Status = status
            };
        }

        public static ServiceResult<T> Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                },
                Status = status
            };
        }

        public static ServiceResult<T> Error(string title, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = status.GetHashCode()
                },
                Status = status
            };
        }

        public static ServiceResult<T> ErrorFromValdation(IDictionary<string, object> errors)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured.",
                    Detail = "See the errors property for details.",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors
                },
                Status = HttpStatusCode.BadRequest
            };
        }
    }
}
