using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string KullaniciHataliGiris = "Kullanıcı Adı veya Parola Hatalı!";
        public static string KullaniciMevcut = "Bu mail zaten kullanılıyor!";
        public static string AracEklemeBasarisiz = "Araç ekleme başarısız oldu!";
        public static string GuncellemeBasarisiz = "Güncelleme başarısız oldu!";
        public static string AracBulunamadi = "Araç bulunamadı!";
    }
}
