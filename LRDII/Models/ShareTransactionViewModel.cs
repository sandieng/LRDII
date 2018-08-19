using LRDII.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class ShareTransactionViewModel : TransactionViewModel
    {     
        [Required]
        [EnumDataType(typeof(ShareTransactionType))]
        [Display(Name = "Jenis Transaksi")]
        public ShareTransactionType JenisTransaksi { get; set; }

        [Required]
        [Display(Name = "Jumlah Saham")]
        [Range(1, 1000000)]
        public int JumlahSaham { get; set; }

        [Display(Name = "Harga Saham")]
        public int NomorHargaSaham { get; set; }

        public SelectList DaftarHargaSaham { get; set; }
    }
}
