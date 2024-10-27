namespace LPMS.Domain.Models.Entities;

public partial class EmailHistory : IAuditableEntity
{
    public int EmailHistoryID { get; set; }

    public string Template { get; set; } = null!;

    public string From { get; set; } = null!;

    public string To { get; set; } = null!;

    public string? CC { get; set; }

    public string? BCC { get; set; }

    public string Body { get; set; } = null!;

    public string? Attachments { get; set; }

    public bool? IsSent { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUTC { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOnUTC { get; set; }
}
