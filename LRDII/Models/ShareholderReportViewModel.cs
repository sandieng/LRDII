using LRDII.Infrastructure;
using System;

namespace LRDII.Models
{
    public class ShareholderReportViewModel
    {
        public int NomorAnggota { get; set; }
        public string NamaLengkap { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public ShareTransactionType JenisTransaksi { get; set; }
        public int JumlahSaham { get; set; }
        public double HargaSaham { get; set; }
        public int TotalSaham { get; set; }
        public double TotalNilaiSaham { get; set; }    
    }
}
