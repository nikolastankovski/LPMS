namespace LPMS.Domain.Models.CustomModels
{
    public record CRUDResult(List<string>? Errors = null, bool IsSuccess = true);
}