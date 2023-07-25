using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class AracIlanDto
    {
        public IlanDto Ilan { get; set; }
        public AracDto Arac { get; set; }
        public List<string?> Resimler { get; set; }
    }
}
