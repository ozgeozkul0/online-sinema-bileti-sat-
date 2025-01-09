using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static ConsoleApp4.program;

namespace ConsoleApp4
{
    public class program
    {
        static void Main(string[] args)
        {
            // Başlık yazar
            Console.Title = "Bilet satış noktası";
            // Yazı rengini mavi yapar
            Console.ForegroundColor = ConsoleColor.Blue;

            // Arka plan rengini beyaz yapar
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("FilmBox'a hoş geldiniz! Vizyonda olan filmlerimizden izlemek istediğiniz filme seçiniz. ");
            // Enum ile sıralanan filmleri gösterir
            foreach (vizyonFilmleri vizyonfilmler in Enum.GetValues(typeof(vizyonFilmleri)))
            {
                Console.WriteLine($"{vizyonfilmler}: {(int)vizyonfilmler}");
            }
            // Filmi numara ile seçtirir
            Console.WriteLine("Seçtiğiniz filmin numarasını giriniz: ");
            int secim = Convert.ToInt32(Console.ReadLine());
            // Filmlerin seansları
            var filmSeanslari = new Dictionary<vizyonFilmleri, string[]>
        {
            { vizyonFilmleri.Dune, new string[] { "12:00", "15:00", "18:00", "21:00" } },
            { vizyonFilmleri.LaLaLand, new string[] { "13:00", "16:00", "19:00", "22:00" } },
            { vizyonFilmleri.MammaMia, new string[] { "14:00", "17:00", "20:00", "23:00" } },
            { vizyonFilmleri.Cars, new string[] { "11:00", "14:30", "18:30", "21:30" } },
            { vizyonFilmleri.Frozen, new string[] { "12:30", "15:30", "19:30", "22:30" } }
        };
            if (Enum.IsDefined(typeof(vizyonFilmleri), secim))
            {
                var secilenFilm = (vizyonFilmleri)secim;
                Console.WriteLine($"{secilenFilm} için seans saatleri:");

                for (int i = 0; i < filmSeanslari[secilenFilm].Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {filmSeanslari[secilenFilm][i]}");
                }

                Console.WriteLine("Seçmek istediğiniz seans numarasını giriniz: ");
                int seansSecim = Convert.ToInt32(Console.ReadLine());

                if (seansSecim >= 1 && seansSecim <= filmSeanslari[secilenFilm].Length)
                {
                    Console.WriteLine($"Seçilen seans: {filmSeanslari[secilenFilm][seansSecim - 1]}");
                }
                else
                {
                    Console.WriteLine("Geçersiz seans numarası girdiniz.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz film numarası girdiniz.");
            }
            //Sınıflardan nesne oluşturulur ve yazdırılır
            DilSec dil = new DilSec();
            dil.veri();
            koltuk koltuk = new koltuk();
            koltuk.veri();
            Fiyat fiyat = new Fiyat();
            fiyat.HesaplaFiyat();
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            odeme odemeIslemi = new kredikart();
            odemeIslemi.kartdetay();
            Console.WriteLine("bilet alım işleminiz tamamlandı! iyi seyirler.");
            Console.ResetColor();
        }

        public enum vizyonFilmleri
        {
            Dune = 1,
            LaLaLand = 2,
            MammaMia = 3,
            Cars,
            Frozen,
        }
        public class yanit
        {
            public virtual void veri()
            {
                Console.WriteLine("Base sınıfı: VeriAl() metodu çağrıldı.");
            }
        }
        public class koltuk : yanit
        {
            public override void veri()
            {
                int koltuknum;
                string koltuksira;

                // Koltuk numarası için geçerli bir sayı girişi alınması
                while (true)
                {
                    Console.WriteLine("Koltuk numaranızı giriniz (sadece sayı): ");
                    string inputNum = Console.ReadLine();

                    if (int.TryParse(inputNum, out koltuknum))
                    {
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Lütfen geçerli bir koltuk numarası giriniz.");
                    }
                }

                // Koltuk sırası için geçerli bir harf girişi alınması
                while (true)
                {
                    Console.WriteLine("A-Z arası koltuk sırasını giriniz (sadece harf): ");
                    koltuksira = Console.ReadLine();

                    if (koltuksira.Length == 1 && char.IsLetter(koltuksira[0]))
                    {
                        koltuksira = koltuksira.ToUpper(); 
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Lütfen geçerli bir koltuk sırası harfi giriniz (A-Z).");
                    }
                }
                Console.WriteLine("Koltugunuz: " + koltuknum + "-" + koltuksira);
            }
        }
        //mısır seçimi ve fiyat 
        public class patlamisMisir : yanit
        {
            public string misir { get; set; }
            public decimal Mfiyat { get; set; }
            public override void veri()
            {
                Console.WriteLine("mısır ister misiniz? isterseniz hangi boy olsun (büyük/orta/küçük) (istemiyorsanız 'x' tuşlayarak devam edebilirsiniz)");
                misir = Console.ReadLine().ToLower();
                switch (misir)
                {
                    case "küçük":
                        Mfiyat = 80;
                        Console.WriteLine("Küçük mısır biletinize eklendi. 80 TL");
                        break;
                    case "büyük":
                        Mfiyat = 120;
                        Console.WriteLine("Büyük mısır biletinize eklendi. 120 TL");
                        break;
                    case "orta":
                        Mfiyat = 100;
                        Console.WriteLine("Orta mısır biletinize eklendi. 100 TL");
                        break;
                    case "x":
                        Console.WriteLine("Mısırsız devam ediyorsunuz.");
                        Mfiyat = 0;
                        break;
                    default:
                        Console.WriteLine("Geçersiz bir seçenek girdiniz.");
                        Mfiyat = 0;
                        break;
                }
            }
        }
        // film dilinin seçilmesi
        public class DilSec : yanit
        {
            public override void veri()
            {
                Console.WriteLine("Lütfen dil tercihinizi yapın." +
                    " orijinal dili-Altyazılı(A)/ Dublaj(D): ");
                string dil = Console.ReadLine();
                if (dil == "A" || dil == "a")
                {
                    Console.WriteLine("filminiz orijinal dilinde-altyazılı seçildi.");
                }
                else if (dil == "D" || dil == "d")
                {
                    Console.WriteLine("filminiz dublajlı seçildi.");
                }
                else
                {
                    Console.WriteLine("geçersiz.");
                }

            }
        }
        public partial class Fiyat : patlamisMisir
        {
            public int filmFiyati = 100;

            public decimal FilmFiyatHesapla()
            {
                return filmFiyati;
            }
            public decimal misirFiyati()
            {
                return Mfiyat;
            }
        }
        public partial class Fiyat 
        {
            public decimal ToplamFiyat { get; set; }

            // Öğrenci indirimi uygulama
            public void Indirim()
            {
                Console.WriteLine("Öğrenci misiniz? (evet/hayır)");
                string ogrenciCevap = Console.ReadLine().ToLower();

                if (ogrenciCevap == "evet")
                {
                    decimal indirim = 25; // Öğrenci indirimi sabit olarak 25 TL
                    ToplamFiyat -= indirim;
                    Console.WriteLine(" 25 TL öğrenci indirimi uygulandı.");
                }
                else
                {
                    Console.WriteLine("Öğrenci indirimi uygulanmadı.");
                }
            }

            // Toplam fiyatı hesapla ve göster
            public void HesaplaFiyat() 
            {
                veri(); // Mısır fiyatını almak için veri() metodunu çağırıyoruz

                // Film fiyatı ve mısır fiyatını topluyoruz
                Console.WriteLine("Film Fiyatı: " + FilmFiyatHesapla());
                Console.WriteLine("Mısır Fiyatı: " + misirFiyati());

                // Toplam fiyat (indirim uygulanmadan önce)
                decimal toplamfiyat = misirFiyati() + FilmFiyatHesapla(); // Toplam fiyatı hesaplıyoruz
                Console.WriteLine("Toplam fiyat (indirim uygulanmadan önce): " + toplamfiyat);
                // Toplam fiyatı sınıf seviyesinde saklıyoruz
                ToplamFiyat = toplamfiyat;
                Indirim();
                // Son fiyatı hesaplıyoruz
                Console.WriteLine("Son Fiyat: " + SonUcret());
            }
        }

        public partial class Fiyat
        {
            public decimal SonUcret()
            {
                return ToplamFiyat; // Son fiyat, ToplamFiyat üzerinden hesaplanacak
            }
        }
    }
    public abstract class odeme
    {
        //Kullanıcıdaan kart detaylarını doğru formda alma
        public abstract void kartdetay();
        protected bool ValidateCardNumber(string cardNumber)
        {
            if (cardNumber.Length == 16)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Hata: Kart numarası 16 haneli olmalıdır.");
                return false;
            }
        }
    }
    //ödeme detaylarını alma
    public class kredikart : odeme
    {
        private string cardNumber;
        private string cardHolderName;
        private string expirationDate;
        private string cvv;

        public override void kartdetay()
        {
            bool isValid = false;
            do
            {
                Console.WriteLine("Kredi kartı numarasını girin (16 haneli):");
                cardNumber = Console.ReadLine();

                isValid = ValidateCardNumber(cardNumber);
                if (!isValid)
                {
                    Console.WriteLine("Lütfen geçerli bir 16 haneli kart numarası girin.");
                }

            } while (!isValid);

            Console.WriteLine("Kart numarası başarıyla alındı.");

            // Kart sahibinin adını uygun şekilde doğrulama 
            do
            {
                Console.WriteLine("Kart Sahibi Adı ve Soyadı Girin (sadece harfler):");
                cardHolderName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(cardHolderName) || !cardHolderName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    Console.WriteLine("Lütfen geçerli bir isim girin (sadece harfler ve boşluk olabilir).");
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }

            } while (!isValid);

            // Son kullanma tarihi doğrulaması (MM/YY formatı)
            isValid = false;
            do
            {
                Console.WriteLine("Son Kullanma Tarihini Girin (MM/YY):");
                expirationDate = Console.ReadLine();

                if (expirationDate.Length == 5 && expirationDate[2] == '/')
                {
                    string[] dateParts = expirationDate.Split('/');
                    if (dateParts.Length == 2 && int.TryParse(dateParts[0], out int month) && int.TryParse(dateParts[1], out int year))
                    {
                        if (month >= 1 && month <= 12)
                        {
                            isValid = true; // Format doğru, geçerli ay
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz ay. Lütfen 01-12 arası bir ay girin.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz format. Lütfen MM/YY formatında bir tarih girin.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz format. Lütfen MM/YY formatında bir tarih girin.");
                }

            } while (!isValid);

            Console.WriteLine("Son kullanma tarihi başarıyla alındı.");

            // CVV doğrulaması 
            isValid = false;
            do
            {
                Console.WriteLine("CVV Kodunu Girin (3 haneli sayı):");
                cvv = Console.ReadLine();

                if (cvv.Length == 3 && int.TryParse(cvv, out int cvvNumber))
                {
                    isValid = true; // Geçerli CVV
                }
                else
                {
                    Console.WriteLine("Geçersiz CVV. Lütfen 3 haneli bir sayı girin.");
                }

            } while (!isValid);

            Console.WriteLine("CVV kodu başarıyla alındı.");
        }
    }
}