namespace LPMS.Application.Models.CustomModels
{
    public class BadRequestModel
    {
        public string type { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public int status { get; set; }
        public List<string> errorMessages { get; set; } = new List<string>();
    }
}
