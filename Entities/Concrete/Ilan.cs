using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Ilan : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AracId { get; set; }
        public string? TelefonNo { get; set; }
        public string? Aciklama { get; set; }
        public DateTime? Tarih { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public bool Durum { get; set; }
    }
}
