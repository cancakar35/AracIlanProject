using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class AracDto : IDto
    {
        public string Kategori { get; set; }
        public string Marka { get; set; }
        public string? Model { get; set; }
        public string? Seri { get; set; }
        public Int16 UretimYili { get; set; }
        public string Renk { get; set; }
        public Int16? MotorGucuHP { get; set; }
        public int? MotorHacmiCC { get; set; }
        public int Kilometre { get; set; }
        public string? VitesTipi { get; set; }
        public string? YakitTipi { get; set; }
        public string? CekisTipi { get; set; }
        public string? KasaTipi { get; set; }
        public bool Sifir { get; set; }
        public decimal Fiyat { get; set; }
    }
}
