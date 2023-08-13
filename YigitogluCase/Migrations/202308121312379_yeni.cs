namespace YigitogluCase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeni : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.YazarTakip", newName: "KullaniciKullanicis");
            RenameColumn(table: "dbo.KullaniciKullanicis", name: "YazarID", newName: "Kullanici_KullaniciId");
            RenameColumn(table: "dbo.KullaniciKullanicis", name: "KullaniciID", newName: "Kullanici_KullaniciId1");
            RenameIndex(table: "dbo.KullaniciKullanicis", name: "IX_YazarID", newName: "IX_Kullanici_KullaniciId");
            RenameIndex(table: "dbo.KullaniciKullanicis", name: "IX_KullaniciID", newName: "IX_Kullanici_KullaniciId1");
            CreateTable(
                "dbo.YazarTakip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YazarId = c.Int(nullable: false),
                        KullaniciId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YazarTakip");
            RenameIndex(table: "dbo.KullaniciKullanicis", name: "IX_Kullanici_KullaniciId1", newName: "IX_KullaniciID");
            RenameIndex(table: "dbo.KullaniciKullanicis", name: "IX_Kullanici_KullaniciId", newName: "IX_YazarID");
            RenameColumn(table: "dbo.KullaniciKullanicis", name: "Kullanici_KullaniciId1", newName: "KullaniciID");
            RenameColumn(table: "dbo.KullaniciKullanicis", name: "Kullanici_KullaniciId", newName: "YazarID");
            RenameTable(name: "dbo.KullaniciKullanicis", newName: "YazarTakip");
        }
    }
}
