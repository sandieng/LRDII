using LRDII.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class ShareReportViewModel
    {
        [Display(Name = "Jenis Laporan")]
        public ShareReportType ReportType { get; set; }
    }
}
    