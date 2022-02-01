using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isOne)
            {
                if (isOne) return "Böyle bir kategori bulunamadı";
                return "Hiç bir kategori bulunamadı.";
            }
            public static string Add(string categoryName)
            {
                return $"{categoryName} başarıyla oluşturuldu";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellendi";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silindi.";
            }
            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellendi";
            }
        }

        public static class Article
        {
            public static string Add(string title)
            {
                return $"{title} başlıklı makale oluşturuldu";
            }
            public static string Delete(string title)
            {
                return $"{title} başlıklı makale güncellendi";
            }
            public static string NotFound(bool isOne)
            {
                if(isOne) return "Böyle bir makale bulunamadı";
                return "Hiç bir makale bulunamadı.";
            }
            public static string Update(string title)
            {
                return $"{title} başlıklı makale güncellendi.";
            }
            public static string HardDelete(string title)
            {
                return $"{title} başlıklı makale silindi.";
            }

        }
    }
}
