using System.ComponentModel.DataAnnotations;

namespace LRDII.Infrastructure
{
    public enum ShareTransactionType
    {
        [Display(Name = "Beli Saham")]
        BeliSaham = 10,

        [Display(Name = "Jual Saham")]
        JualSaham = 11,
    }
}
