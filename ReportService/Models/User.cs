using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReportService.Models;

namespace PersonService.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public string LocationInformation { get; set; }
        public int RegisteredPersonCount { get; set; }
        public int RegisteredPhoneNumberCount { get; set; }

        public List<ReportDetail> ReportDetail { get; set; }
    }
}
