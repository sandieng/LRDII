using System.ComponentModel.DataAnnotations;

namespace LRDII.Infrastructure
{
    public enum LoanTransactionType
    {     
        [Display(Name = "Pinjaman Uang")]
        PinjamanUang = 20,

        [Display(Name = "Pembayaran Pinjaman")]
        PembayatanPinjaman = 21
    }
}
