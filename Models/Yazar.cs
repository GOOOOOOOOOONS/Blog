namespace mBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yazar")]
    public partial class Yazar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yazar()
        {
            Makales = new HashSet<Makale>();
            YazarYetkis = new HashSet<YazarYetki>();
            Yazar1 = new HashSet<Yazar>();
            Yazars = new HashSet<Yazar>();
        }

        public int YazarId { get; set; }

        [Required]
        [StringLength(50)]
        public string YazarAdi { get; set; }

        [Required]
        [StringLength(50)]
        public string YazarSoyad { get; set; }

        [Required]
        [StringLength(50)]
        public string YazarKullaniciAdi { get; set; }

        [Required]
        [StringLength(100)]
        public string YazarSifre { get; set; }

        [Required]
        [StringLength(50)]
        public string YazarMailAdres { get; set; }

        public string YazarAciklama { get; set; }

        public int YetkiID { get; set; }

        public int? YazarYetki { get; set; }

        public DateTime? YazarEklenmeTarihi { get; set; }

        [StringLength(50)]
        public string YazarFace { get; set; }

        [StringLength(50)]
        public string YazarInstagram { get; set; }

        [StringLength(50)]
        public string YazarLinked { get; set; }

        [StringLength(50)]
        public string YazarGithub { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makale> Makales { get; set; }

        public virtual Message Message { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YazarYetki> YazarYetkis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yazar> Yazar1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yazar> Yazars { get; set; }
    }
}
