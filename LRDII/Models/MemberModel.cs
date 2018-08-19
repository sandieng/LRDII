using LRDII.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace LRDII.Models
{
    public class MemberModel
    {
        [Key]
        [Display(Name ="Nomor Anggota")]
        public int NomorAnggota { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nama Lengkap Anggota")]
        public string NamaLengkap { get; set; }
      

        [Required]
        [MaxLength(300)]
        [Display(Name = "Alamat Lengkap")]
        public string AlamatLengkap { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        [Display(Name = "Nomor HP/Mobile")]
        public string NomorHp { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
    

        [Required]
        [EnumDataType(typeof(MembershipType))]
        [Display(Name = "Jenis Anggota")]
        public MembershipType JenisAnggota { get; set; }

        //   [HiddenInput(DisplayValue = false)]
        [Display(Name = "Anggota Aktif")]
        [EnumDataType(typeof(YesNoType))]
        public YesNoType IsActive { get; set; } = YesNoType.Yes;

       // public ICollection<ShareTransactionModel> shareTransactions { get; set; }
    }
}
    