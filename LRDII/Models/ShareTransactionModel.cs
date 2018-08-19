using LRDII.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class ShareTransactionModel
    {
        [Key]
        public int NomorTransaksi { get; set; }
        public int NomorAnggota { get; set; }
        public int NomorHargaSaham { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public ShareTransactionType JenisTransaksi { get; set; }
        public int JumlahSaham { get; set; }
    }
}
