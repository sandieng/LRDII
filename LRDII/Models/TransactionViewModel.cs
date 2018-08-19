using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class TransactionViewModel
    {
        [Required]
        [Display(Name = "Nama Anggota")]
        public int NomorAnggota { get; set; }

        public SelectList DaftarAnggota { get; set; }

        [Required]
        [Display(Name = "Tanggal Transaksi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime TanggalTransaksi { get; set; } = DateTime.Now.Date;

    }
}
