using LRDII.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class GoodsTransactionModel
    {                
        [Key]
        public int NomorTransaksi { get; set; }
        public int NomorAnggota { get; set; }
        public DateTime TanggalTransaksi { get; set; }
        public GoodsTransactionType JenisTransaksi { get; set; }
        public double HargaBarangJasa { get; set; }
        public int Jumlah { get; set; }
        public int TotalHarga { get; set; }
    }
}
