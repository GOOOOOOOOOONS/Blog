namespace mBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YazarYetki")]
    public partial class YazarYetki
    {
        public int YazarYetkiId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int YetkiID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int YazarID { get; set; }

        public virtual Yazar Yazar { get; set; }

        public virtual Yetki Yetki { get; set; }
    }
}
