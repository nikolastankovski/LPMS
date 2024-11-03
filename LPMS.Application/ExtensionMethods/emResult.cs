using FluentResults;
using Microsoft.AspNetCore.Http;

namespace LPMS.Application.ExtensionMethods
{
    public static class emResult
    {
        public static IResult ToBadRequest<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert Success result to Failure!");

            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad Request",
                type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                extensions: new Dictionary<string, object?>
                {
                    { "errorMessages", result.Errors.Select(x => x.Message).ToList() }
                }
            );
        }

        public static IResult ToBadRequest(this Result result)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cannot convert Success result to Failure!");

            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad Request",
                type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                extensions: new Dictionary<string, object?>
                {
                    { "errorMessages", result.Errors.Select(x => x.Message).ToList() }
                }
            );
        }

        public static IResult ToOkResponse<T>(this Result<T> result)
        {
            if (result.IsFailed)
                throw new InvalidOperationException("Cannot convert Failed result to Success!");

            return Results.Ok(result.Value);
        }

        public static IResult ToOkResponse(this Result result)
        {
            if (result.IsFailed)
                throw new InvalidOperationException("Cannot convert Failed result to Success!");

            return Results.Ok();
        }

        public static IResult ToCreatedResponse<T>(this Result<T> result, string uri)
        {
            if (result.IsFailed)
                throw new InvalidOperationException("Cannot convert Failed result to Success!");

            return Results.Created(uri, result.Value);
        }
    }
}