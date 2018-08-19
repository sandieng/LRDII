using LRDII.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class GoodsTransactionViewModel : TransactionViewModel
    {                
        [Required]
        [EnumDataType(typeof(GoodsTransactionType))]
        [Display(Name = "Jenis Transaksi")]
        public GoodsTransactionType JenisTransaksi { get; set; }

        [Required]
        [Display(Name = "Harga Barang/Jasa")]
        [Range(1, 100000000)]
        public double HargaBarangJasa { get; set; }      
    }
}
