using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class LoanRepaymentTransactionModel
    {
        [Key]
        public int NomorPembayaranPinjaman { get; set; }
        public int NomorPinjaman { get; set; }
        public int NomorAnggota { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public double JumlahPinjamanPokok { get; set; }
        public double JumlahBungaPinjaman { get; set; }
    }
}
