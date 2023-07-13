using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Motosiklet : IEntity
    {
        public int AracId { get; set; }
        public int? KategoriId { get; set; }
        public int? VitesTipiId { get; set; }
        public int? YakitTipiId { get; set; }
    }
}
