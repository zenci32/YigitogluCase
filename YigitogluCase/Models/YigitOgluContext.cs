using System.Data.Entity;
using YigitogluCase.Models.Mapping;

namespace YigitogluCase.Models
{
    public partial class YigitOgluContext : DbContext
    {
        static YigitOgluContext()
        {
            Database.SetInitializer<YigitOgluContext>(null);
        }

        public YigitOgluContext()
            : base("Name=YigitOgluContext")
        {
        }

        public DbSet<Etiket> Etikets { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Kullanici> Kullanicis { get; set; }
        public DbSet<KullaniciRol> KullaniciRols { get; set; }
        public DbSet<Makale> Makales { get; set; }
        public DbSet<Resim> Resims { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Yazar> Yazars { get; set; }
        public DbSet<Yorum> Yorums { get; set; }

        public DbSet<YazarTakip> YazarTakips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EtiketMap());
            modelBuilder.Configurations.Add(new KategoriMap());
            modelBuilder.Configurations.Add(new KullaniciMap());
            modelBuilder.Configurations.Add(new KullaniciRolMap());
            modelBuilder.Configurations.Add(new MakaleMap());
            modelBuilder.Configurations.Add(new ResimMap());
            modelBuilder.Configurations.Add(new RolMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new YazarMap());
            modelBuilder.Configurations.Add(new YorumMap());
            modelBuilder.Configurations.Add(new YazarTakipMap());
        }
    }
}
