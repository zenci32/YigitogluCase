using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace YigitogluCase.Models.Mapping
{
    public class YazarTakipMap : EntityTypeConfiguration<YazarTakip>
    {

        public YazarTakipMap()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.YazarId)
                .IsRequired();
            this.Property(t => t.KullaniciId)
                .IsRequired();

            
            this.ToTable("YazarTakip");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.YazarId).HasColumnName("YazarId");
            this.Property(t => t.KullaniciId).HasColumnName("KullaniciId");
        }

    }
}