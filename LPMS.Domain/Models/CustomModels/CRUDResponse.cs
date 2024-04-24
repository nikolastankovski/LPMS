namespace LPMS.Domain.Models.CustomModels
{
    public class CRUDResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
