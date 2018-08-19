using System;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class SharePriceModel
    {
        [Key]
        [Display(Name = "Nomor Harga Saham")]
        public int NomorHargaSaham { get; set; }

        [Required]
        [Display(Name = "Tanggal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime TanggalHarga { get; set; } = DateTime.Now.Date;

        [Required]
        [Display(Name ="Harga Saham")]
        public double HargaSaham { get; set; }
    }
}
