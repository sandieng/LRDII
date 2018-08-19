using System.ComponentModel.DataAnnotations;

namespace LRDII.Infrastructure
{
    public enum InterestTermType
    {
        [Display(Name = "1 Bulan")]
        SatuBulan = 1,

        [Display(Name = "3 Bulan")]
        TigaBulan = 3,

        [Display(Name = "6 Bulan")]
        EnamBulan = 6,

        [Display(Name = "12 Bulan")]
        SatuTahun = 12
    }
}
