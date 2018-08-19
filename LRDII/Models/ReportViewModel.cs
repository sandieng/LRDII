using LRDII.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class ReportViewModel
    {
        [Display(Name = "Jenis Laporan")]
        public LoanReportType ReportType { get; set; }
    }
}
    