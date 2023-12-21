using System;
using System.Collections.Generic;

namespace LPMS.Domain.DbModels;

public partial class Client
{
    public Guid ClientId { get; set; }

    public string? Code { get; set; }

    public string? Forename { get; set; }

    public string? Surname { get; set; }

    public Guid? GenderId { get; set; }

    public Guid? NationalityId { get; set; }

    public Guid? IdDocumentTypeId { get; set; }

    public string? IdDocumentNumber { get; set; }

    public DateOnly? IdDocumentExpiryDate { get; set; }

    public string? LegalName { get; set; }

    public string? TradeName { get; set; }

    public string? UniqueIdentificationNumber { get; set; }

    public DateOnly? EstablishDate { get; set; }

    public string? Address { get; set; }

    public string? Address2 { get; set; }

    public Guid? CityId { get; set; }

    public Guid? CountryId { get; set; }

    public string? Email { get; set; }

    public string? Email2 { get; set; }

    public string? Phone { get; set; }

    public string? Phone2 { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
