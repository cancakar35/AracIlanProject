using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AracResim : IEntity
    {
        public int Id { get; set; }
        public int AracId { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
