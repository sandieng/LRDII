using System.ComponentModel.DataAnnotations;

namespace LRDII.Infrastructure
{
    public enum ShareReportType
    {
        [Display(Name = "Laporan Harga Saham")]
        SharePriceReport,

        [Display(Name = "Laporan Pemegang Saham")]
        ShareholderReport,

        [Display(Name = "Laporan Penjualan Saham")]
        ShareSaleReport
    }
}
