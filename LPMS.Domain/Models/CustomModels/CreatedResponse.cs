namespace LPMS.Domain.Models.CustomModels
{
    public class CreatedResponse<T> where T : struct
    {
        public CreatedResponse(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public CreatedResponse(bool isSuccess, T id)
        {
            this.isSuccess = isSuccess;
            Id = id;
        }

        public bool isSuccess { get; set; } = false;
        public T Id { get; set; }
    }
}
