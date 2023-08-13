namespace YigitogluCase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Etiket",
                c => new
                    {
                        EtiketId = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.EtiketId);
            
            CreateTable(
                "dbo.Makale",
                c => new
                    {
                        MakaleId = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 100),
                        Icerik = c.String(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        KategoriID = c.Int(nullable: false),
                        GoruntulenmeSayisi = c.Int(nullable: false),
                        Begeni = c.Int(nullable: false),
                        YazarID = c.Int(nullable: false),
                        ResimID = c.Int(),
                    })
                .PrimaryKey(t => t.MakaleId)
                .ForeignKey("dbo.Kategori", t => t.KategoriID, cascadeDelete: true)
                .ForeignKey("dbo.Kullanici", t => t.YazarID, cascadeDelete: true)
                .ForeignKey("dbo.Resim", t => t.ResimID)
                .Index(t => t.KategoriID)
                .Index(t => t.YazarID)
                .Index(t => t.ResimID);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        KategoriId = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.KategoriId);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        KullaniciId = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Soyadi = c.String(nullable: false, maxLength: 50),
                        KullaniciAdi = c.String(nullable: false, maxLength: 50),
                        Parola = c.String(nullable: false, maxLength: 200),
                        Aciklama = c.String(),
                        MailAdres = c.String(nullable: false, maxLength: 50),
                        Cinsiyet = c.Boolean(),
                        DogumTarihi = c.DateTime(),
                        KayitTarihi = c.DateTime(nullable: false),
                        Yazar = c.Boolean(),
                        Onaylandi = c.Boolean(),
                        Aktif = c.Boolean(),
                        ResimID = c.Int(),
                    })
                .PrimaryKey(t => t.KullaniciId)
                .ForeignKey("dbo.Resim", t => t.ResimID)
                .Index(t => t.ResimID);
            
            CreateTable(
                "dbo.KullaniciRol",
                c => new
                    {
                        RolID = c.Int(nullable: false),
                        KullaniciID = c.Int(nullable: false),
                        KullaniciRolID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => new { t.RolID, t.KullaniciID })
                .ForeignKey("dbo.Kullanici", t => t.KullaniciID, cascadeDelete: true)
                .ForeignKey("dbo.Rol", t => t.RolID, cascadeDelete: true)
                .Index(t => t.RolID)
                .Index(t => t.KullaniciID);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        RolID = c.Int(nullable: false, identity: true),
                        RolAdi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RolID);
            
            CreateTable(
                "dbo.Resim",
                c => new
                    {
                        ResimId = c.Int(nullable: false, identity: true),
                        KucukBoyut = c.String(maxLength: 250),
                        OrtaBoyut = c.String(maxLength: 250),
                        BuyukBoyut = c.String(maxLength: 250),
                        Video = c.String(maxLength: 250),
                        MakaleID = c.Int(),
                    })
                .PrimaryKey(t => t.ResimId)
                .ForeignKey("dbo.Makale", t => t.MakaleID)
                .Index(t => t.MakaleID);
            
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        YorumId = c.Int(nullable: false, identity: true),
                        Yorum = c.String(nullable: false, maxLength: 1500),
                        MakaleID = c.Int(nullable: false),
                        EklenmeTarihi = c.DateTime(),
                        AdSoyad = c.String(nullable: false, maxLength: 150),
                        Begeni = c.Int(),
                    })
                .PrimaryKey(t => t.YorumId)
                .ForeignKey("dbo.Makale", t => t.MakaleID, cascadeDelete: true)
                .Index(t => t.MakaleID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.Yazar",
                c => new
                    {
                        YazarId = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Soyadi = c.String(nullable: false, maxLength: 50),
                        KullaniciAdi = c.String(nullable: false, maxLength: 50),
                        Parola = c.String(nullable: false, maxLength: 50),
                        MailAdres = c.String(maxLength: 50),
                        Aciklama = c.String(),
                        Cinsiyet = c.Boolean(),
                        ReismID = c.Int(),
                        Onaylandi = c.Boolean(),
                    })
                .PrimaryKey(t => t.YazarId);
            
            CreateTable(
                "dbo.YazarTakip",
                c => new
                    {
                        YazarID = c.Int(nullable: false),
                        KullaniciID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YazarID, t.KullaniciID })
                .ForeignKey("dbo.Kullanici", t => t.YazarID)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciID)
                .Index(t => t.YazarID)
                .Index(t => t.KullaniciID);
            
            CreateTable(
                "dbo.MakaleEtiket",
                c => new
                    {
                        EtiketId = c.Int(nullable: false),
                        MakaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EtiketId, t.MakaleId })
                .ForeignKey("dbo.Etiket", t => t.EtiketId, cascadeDelete: true)
                .ForeignKey("dbo.Makale", t => t.MakaleId, cascadeDelete: true)
                .Index(t => t.EtiketId)
                .Index(t => t.MakaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MakaleEtiket", "MakaleId", "dbo.Makale");
            DropForeignKey("dbo.MakaleEtiket", "EtiketId", "dbo.Etiket");
            DropForeignKey("dbo.Yorum", "MakaleID", "dbo.Makale");
            DropForeignKey("dbo.Makale", "ResimID", "dbo.Resim");
            DropForeignKey("dbo.Makale", "YazarID", "dbo.Kullanici");
            DropForeignKey("dbo.Kullanici", "ResimID", "dbo.Resim");
            DropForeignKey("dbo.Resim", "MakaleID", "dbo.Makale");
            DropForeignKey("dbo.KullaniciRol", "RolID", "dbo.Rol");
            DropForeignKey("dbo.KullaniciRol", "KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.YazarTakip", "KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.YazarTakip", "YazarID", "dbo.Kullanici");
            DropForeignKey("dbo.Makale", "KategoriID", "dbo.Kategori");
            DropIndex("dbo.MakaleEtiket", new[] { "MakaleId" });
            DropIndex("dbo.MakaleEtiket", new[] { "EtiketId" });
            DropIndex("dbo.YazarTakip", new[] { "KullaniciID" });
            DropIndex("dbo.YazarTakip", new[] { "YazarID" });
            DropIndex("dbo.Yorum", new[] { "MakaleID" });
            DropIndex("dbo.Resim", new[] { "MakaleID" });
            DropIndex("dbo.KullaniciRol", new[] { "KullaniciID" });
            DropIndex("dbo.KullaniciRol", new[] { "RolID" });
            DropIndex("dbo.Kullanici", new[] { "ResimID" });
            DropIndex("dbo.Makale", new[] { "ResimID" });
            DropIndex("dbo.Makale", new[] { "YazarID" });
            DropIndex("dbo.Makale", new[] { "KategoriID" });
            DropTable("dbo.MakaleEtiket");
            DropTable("dbo.YazarTakip");
            DropTable("dbo.Yazar");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Yorum");
            DropTable("dbo.Resim");
            DropTable("dbo.Rol");
            DropTable("dbo.KullaniciRol");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Kategori");
            DropTable("dbo.Makale");
            DropTable("dbo.Etiket");
        }
    }
}
