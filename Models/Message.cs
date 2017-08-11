namespace mBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int MessageId { get; set; }

        [StringLength(500)]
        public string MessageAciklama { get; set; }

        [StringLength(50)]
        public string MessageGonderenAd { get; set; }

        [StringLength(50)]
        public string MessageGonderenSoyad { get; set; }

        public DateTime? MessageGondermeTarih { get; set; }

        public int? MessageYazarID { get; set; }

        public virtual Yazar Yazar { get; set; }
    }
}
