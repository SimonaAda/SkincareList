// See https://aka.ms/new-console-template for more information
using pF;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

public class Program
{
    static List<Skincare> produkty = new List<Skincare>();
    static string defaultFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Produkty");
    static string defaultFilePath = Path.Combine(defaultFolderPath, "produkty.xml");

    
    static void Main(string[] args)
    {
        

        if (!Directory.Exists(defaultFolderPath))
        {
            Directory.CreateDirectory(defaultFolderPath);
        }

     //   Xml.ImportFromXml(defaultFilePath);

        while (true)
        {
            Console.WriteLine("Zvolte možnost:");
            Console.WriteLine("1 - Přidej produkt.");
            Console.WriteLine("2 - Třiď produkty podle účinku.");
            Console.WriteLine("3 - Kontrola expirace.");
            Console.WriteLine("4 - Seznam produktů.");
            Console.WriteLine("5 - Konec.");

            Console.WriteLine();

            var moznost = Console.ReadLine();

            switch (moznost)
            {
                case "1":
                    PridejProdukt();
                    break;
                case "2":
                    TridPodleUcinku();
                    break;
                case "3":
                    SkontrolujExpiraci();
                    break;
                case "4":
                    ZobrazProdukty();
                    break;
                case "5":
                    UlozAUkonci();
                    return;
                default:
                    Console.WriteLine("Nesprávny vstup. Skúste to znovu.");
                    break;
            }


        }
    }

    static void PridejProdukt()
    {
        Console.WriteLine("Zadejte název produktu: ");
        var nazev = Console.ReadLine();
        Console.WriteLine("Účinek: ");
        var ucinek = Console.ReadLine();
        Console.WriteLine("Datum otevření (rok/měsíc/den): ");
        DateTime datumOtevreni = DateTime.Parse(Console.ReadLine());

        DateTime datumExpirace = datumOtevreni.AddMonths(12);

        Skincare produkt = new Skincare();
        produkty.Add(produkt);
        
    }


    static void TridPodleUcinku()
    {
        Console.WriteLine("Zadejte účinek: ");
        string ucinek = Console.ReadLine();
        var vyfiltrovaneProdukty = produkty.Where(p => p.Ucinek.Equals(ucinek)).ToList();

        if(vyfiltrovaneProdukty.Count > 0)
        {
            foreach (var produkt in vyfiltrovaneProdukty)
            {
                Console.WriteLine($"{ucinek} účinek: {produkt}.");
            }
        }
        else
        {
            Console.WriteLine($"Filtru {ucinek} účinek neodpovídá žádný produkt.");
        }
    }

    static void SkontrolujExpiraci()
    {
        DateTime dnes = DateTime.Today;
        DateTime dnyDoExpirace = DateTime.Today.AddDays(14);

        var bliziciSeExpirace = produkty.Where(p => p.DatumExpirace <= dnyDoExpirace && p.DatumExpirace >= dnes).ToList();

        if (bliziciSeExpirace.Count > 0)
        {
            Console.WriteLine($"Produkty s blížíce se expirací: ");

            foreach (var produkt in bliziciSeExpirace)
            {
                Console.WriteLine(produkt);
            }
        }

        else
        {
            Console.WriteLine("Seznam neobsahuje žádný produkt s blížící se expirací");
        }
 
    }

        static void ZobrazProdukty()
        {
            foreach (var produkt in produkty)
            {
                if (produkty.Count > 0)
                {
                    Console.WriteLine($"nazev: {produkt.Nazev}, účinek: {produkt.Ucinek},datum otevření: {produkt.DatumOtevreni}");
                }
                else
                {
                    Console.WriteLine("Produkt nenalezen.");
                }

            }
        }

        static void UlozAUkonci()
        {

            Xml.ExportToXml(produkty, defaultFilePath);
            return;
        }




    
}
