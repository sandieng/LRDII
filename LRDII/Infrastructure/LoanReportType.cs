using System.ComponentModel.DataAnnotations;

namespace LRDII.Infrastructure
{
    public enum LoanReportType
    {     
        [Display(Name = "Laporan Pinjaman Uang")]
        LoanReport,

        [Display(Name = "Laporan Pengembalian Pinjaman Uang")]
        LoanRepaymentReport
    }
}
