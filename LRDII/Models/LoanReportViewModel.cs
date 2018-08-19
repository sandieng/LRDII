using LRDII.Infrastructure;
using System;

namespace LRDII.Models
{
    public class LoanReportViewModel
    {
        public int NomorPinjaman { get; set; }
        public int NomorAnggota { get; set; }
        public string NamaLengkap { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public double JumlahPinjaman { get; set; }
        public double PersentaseBunga { get; set; }
        public InterestTermType LamaPinjaman { get; set; }
    }
}
