using FluentEmail.Core.Models;
using System.Globalization;

namespace EmailService
{
    public class EmailSetUp
    {
        public Address? From { get; set; }
        public required Address To { get; set; }
        public List<Address> CC { get; set; } = new List<Address>();
        public List<Address> BCC { get; set; } = new List<Address>();
        public required string Subject { get; set; }
        public required string EmailTemplate { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public required object Tokens { get; set; }
        public required CultureInfo Culture { get; set; }
    }
}
