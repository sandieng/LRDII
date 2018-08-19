using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class EditShareTransactionViewModel
    {
        [Display(Name = "Nomor Transaksi")]
        public int NomorTransaksi { get; set; }

        [Display(Name = "Nama Anggota")]
        public int NomorAnggota { get; set; }
        public SelectList DaftarAnggota { get; set; }

        [Display(Name = "Harga Saham")]
        public int NomorHargaSaham { get; set; }
        public SelectList DaftarHarga { get; set; }

        [Display(Name = "Tanggal Transaksi")]
        public DateTime TanggalTransaksi { get; set; }

        [Display(Name = "Jumlah Saham")]
        public int JumlahSaham { get; set; }
    }
}
