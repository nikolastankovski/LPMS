using System.Globalization;
using LPMS.Domain.Models.RnRModels.AuthModels;

namespace LPMS.API.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointGroup = endpoints.MapGroup("api/{culture}/auth");

        endpointGroup.MapPost("/login",
                async (string culture, LoginRequest request, IAuthService authService) =>
                {
                    var getTokenResult =
                        await authService.GetAuthTokenAsync(request, CultureInfo.GetCultureInfo(culture));

                    return getTokenResult.IsSuccess ? getTokenResult.ToOkResponse() : getTokenResult.ToBadRequest();
                })
            .Produces<AuthTokenResponse>(statusCode: StatusCodes.Status200OK)
            .Produces<BadRequestModel>(statusCode: StatusCodes.Status400BadRequest)
            .Produces<InternalServerErrorModel>(statusCode: StatusCodes.Status500InternalServerError)
            .WithTags(EndpointTags.Auth);

        endpointGroup.MapPost("/refresh-token",
            async (string culture, RefreshTokenRequest request, IAuthService authService) =>
            {
                var refreshAuthToken =
                    await authService.RefreshAuthTokenAsync(request, CultureInfo.GetCultureInfo(culture));

                return refreshAuthToken.IsSuccess ? refreshAuthToken.ToOkResponse() : refreshAuthToken.ToBadRequest();
            })
            .Produces<AuthTokenResponse>(statusCode: StatusCodes.Status200OK)
            .Produces<BadRequestModel>(statusCode: StatusCodes.Status400BadRequest)
            .Produces<InternalServerErrorModel>(statusCode: StatusCodes.Status500InternalServerError)
            .WithTags(EndpointTags.Auth);
    }
}