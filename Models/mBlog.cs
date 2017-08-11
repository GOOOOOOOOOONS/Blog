namespace mBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class mBlog : DbContext
    {
        public mBlog()
            : base("name=mBlog")
        {
        }

        public virtual DbSet<Etiket> Etikets { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Makale> Makales { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Yazar> Yazars { get; set; }
        public virtual DbSet<YazarYetki> YazarYetkis { get; set; }
        public virtual DbSet<Yetki> Yetkis { get; set; }
        public virtual DbSet<Yorum> Yorums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etiket>()
                .HasMany(e => e.Makales)
                .WithMany(e => e.Etikets)
                .Map(m => m.ToTable("MakaleEtiket").MapLeftKey("EtiketID").MapRightKey("MakaleID"));

            modelBuilder.Entity<Makale>()
                .HasMany(e => e.Yorums)
                .WithRequired(e => e.Makale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yazar>()
                .HasMany(e => e.Makales)
                .WithRequired(e => e.Yazar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yazar>()
                .HasOptional(e => e.Message)
                .WithRequired(e => e.Yazar);

            modelBuilder.Entity<Yazar>()
                .HasMany(e => e.YazarYetkis)
                .WithRequired(e => e.Yazar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Yazar>()
                .HasMany(e => e.Yazar1)
                .WithMany(e => e.Yazars)
                .Map(m => m.ToTable("YazarTakip").MapLeftKey("AdminID").MapRightKey("YazarID"));

            modelBuilder.Entity<Yetki>()
                .HasMany(e => e.YazarYetkis)
                .WithRequired(e => e.Yetki)
                .WillCascadeOnDelete(false);
        }
    }
}
