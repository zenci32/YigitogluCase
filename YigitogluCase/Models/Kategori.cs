using System.Collections.Generic;

namespace YigitogluCase.Models
{
    public partial class Kategori
    {
        public Kategori()
        {
            this.Makales = new List<Makale>();
        }

        public int KategoriId { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public virtual ICollection<Makale> Makales { get; set; }
    }
}
