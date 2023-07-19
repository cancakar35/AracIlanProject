using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class AddIlanDto : IDto
    {
        public int? UserId { get; set; }
        public string TelefonNo { get; set; }
        public string? Aciklama { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
    }
}
