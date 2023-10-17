using System.ComponentModel.DataAnnotations;

namespace ReportService.Models
{
    public class ReportDetail
    {
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
        public string Location { get; set; }
        public string PersonName { get; set; }
        public string PhoneNumber { get; set; }
    }
}