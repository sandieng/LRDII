using System;

namespace LRDII.Models
{
    public class LoanRepaymentReportViewModel
    {
        public int NomorPembayaranPinjaman { get; set; }
        public int NomorPinjaman { get; set; }
        public double JumlahPinjaman { get; set; }
        public int NomorAnggota { get; set; }
        public string NamaLengkap { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public double JumlahPinjamanPokok { get; set; }
        public double JumlahBungaPinjaman { get; set; }
    }
}
