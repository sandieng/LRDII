using LRDII.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class LoanTransactionViewModel : TransactionViewModel
    {                
        [Required]
        [EnumDataType(typeof(LoanTransactionType))]
        [Display(Name = "Jenis Transaksi")]
        public LoanTransactionType JenisTransaksi { get; set; }

        // Take out loan      
        [Display(Name = "Jumlah Pinjaman")]
        [Range(1, 100000000)]
        public double JumlahPinjaman { get; set; }      
      
        [Display(Name ="Persentase Bunga")]
        [Range(0.25, 20.99)]
        public double PersentaseBunga { get; set; }
      
        [EnumDataType(typeof(InterestTermType))]
        [Display(Name = "Lama Pinjaman")]
        public InterestTermType LamaPinjaman { get; set; }


        // Repay loan  
        [Display(Name = "Jumlah Pinjaman Pokok")]
        [Range(1, 100000000)]
        public double JumlahPinjamanPokok { get; set; }
     
        [Display(Name = "Jumlah Bunga Pinjaman")]
        [Range(1, 100000000)]
        public double JumlahBungaPinjaman { get; set; }

        [Display(Name = "Nomor Pinjaman")]
        [Range(1, 100000000)]
        public int NomorPinjaman { get; set; }
    }
}
