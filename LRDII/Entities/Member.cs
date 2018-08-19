using System.ComponentModel.DataAnnotations;

namespace LRDII.Entities
{
    public class Member
    {
        [Key]
        public int NomorAnggota { get; set; }
        public string NamaLengkap { get; set; }
        public string AlamatLengkap { get; set; }
        public string NomorHp { get; set; }        
        public string Email { get; set; }
        public string JenisAnggota { get; set; }
        public bool IsActive { get; set; }
    }
}
