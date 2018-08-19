using System.ComponentModel.DataAnnotations;

namespace LRDII.Infrastructure
{
    public enum GoodsTransactionType
    {
        [Display(Name = "Beli Barang/Jasa")]
        BeliBarangJasa = 30,

        [Display(Name = "Jual Barang/Jasa")]
        JualBarangJasa = 31,
    }
}
