using System;
using System.Collections.Generic;

namespace LPMS.Application.Models.Entities;

public partial class Client
{
    public int ClientID { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int ClientTypeId { get; set; }

    public int? GenderId { get; set; }

    public int? NationalityId { get; set; }

    public int? IdDocumentTypeId { get; set; }

    public string? IdDocumentNumber { get; set; }

    public DateOnly? IdDocumentExpiryDate { get; set; }

    public string? LegalName { get; set; }

    public string? TradeName { get; set; }

    public string? UniqueIdentificationNumber { get; set; }

    public DateOnly? EstablishDate { get; set; }

    public string? Address { get; set; }

    public string? Address2 { get; set; }

    public int? CityId { get; set; }

    public int? CountryId { get; set; }

    public string? Email { get; set; }

    public string? Email2 { get; set; }

    public string? Phone { get; set; }

    public string? Phone2 { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool? IsActive { get; set; }
}
