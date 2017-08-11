namespace mBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        public int YorumId { get; set; }

        [Required]
        [StringLength(1500)]
        public string YorumAciklama { get; set; }

        public int MakaleID { get; set; }

        public DateTime? YorumEklenmeTarihi { get; set; }

        [Required]
        [StringLength(50)]
        public string YorumAdSoyad { get; set; }

        [Required]
        [StringLength(50)]
        public string YorumMail { get; set; }

        public int? YorumBegeni { get; set; }

        public virtual Makale Makale { get; set; }
    }
}
