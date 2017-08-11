namespace mBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Footer")]
    public partial class Footer
    {
        public int FooterId { get; set; }

        [StringLength(50)]
        public string FooterAciklama { get; set; }

        [StringLength(50)]
        public string FooterFacebook { get; set; }

        [StringLength(50)]
        public string FooterInstagram { get; set; }
    }
}
