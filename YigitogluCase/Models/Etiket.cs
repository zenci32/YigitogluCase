using System;
using System.Collections.Generic;

namespace YigitogluCase.Models
{
    public partial class Etiket
    {
        public Etiket()
        {
            this.Makales = new List<Makale>();
        }

        public int EtiketId { get; set; }
        public string Adi { get; set; }
        public virtual ICollection<Makale> Makales { get; set; }
    }
}
