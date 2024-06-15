namespace LPMS.Application.Models.CustomModels
{
    public record CRUDResult(List<string>? Errors = null, bool IsSuccess = true);
}