using LRDII.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class LoanTransactionModel
    {                
        [Key]
        public int NomorPinjaman { get; set; }
        public int NomorAnggota { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public double JumlahPinjaman { get; set; }
        public double PersentaseBunga { get; set; }
        public InterestTermType LamaPinjaman { get; set; }
    }
}
