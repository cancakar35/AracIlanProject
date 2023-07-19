using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Arac : IEntity
    {
        public int Id { get; set; }
        public int KategoriId { get; set; }
        public int MarkaId { get; set; }
        public string? Model { get; set; }
        public string? Seri { get; set; }
        public Int16 UretimYili { get; set; }
        public int RenkId { get; set; }
        public Int16? MotorGucuHP { get; set; }
        public int? MotorHacmiCC { get; set; }
        public int KiloMetre { get; set; }
        public int? VitesTipiId { get; set; }
        public int? YakitTipiId { get; set; }
        public int? CekisTipiId { get; set; }
        public int? KasaTipiId { get; set; }
        public bool Sifir { get; set; }
        public decimal Fiyat { get; set; }
    }
}
