// See https://aka.ms/new-console-template for more information
using pF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

public class Program
{
    static List<Skincare> produkty = new List<Skincare>();
    static string defaultFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Produkty");
    static string defaultFilePath = Path.Combine(defaultFolderPath,"produkty.xml");

    
    static void Main(string[] args)
    {
        string filePath = defaultFilePath;
        produkty.AddRange(Xml.ImportFromXml(filePath));

        if (!Directory.Exists(defaultFolderPath))
        {
            Directory.CreateDirectory(defaultFolderPath);
        }

        while (true)
        {
            Console.WriteLine("Zvolte možnost:");
            Console.WriteLine("1 - Přidej produkt.");
            Console.WriteLine("2 - Třiď produkty podle účinku.");
            Console.WriteLine("3 - Kontrola expirace.");
            Console.WriteLine("4 - Seznam produktů.");
            Console.WriteLine("5 - Vymaž produkt.");
            Console.WriteLine("6 - Konec.");

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
                    VymazProdukty();
                    break;
                case "6":
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
        Skincare produkt = new Skincare();

        Console.WriteLine("Zadejte název produktu: ");
        produkt.Nazev = Console.ReadLine();
        Console.WriteLine("Účinek: ");
        produkt.Ucinek = Console.ReadLine();
        Console.WriteLine("Datum otevření (rok/měsíc/den): ");
        produkt.DatumOtevreni = DateTime.Parse(Console.ReadLine());
        
        produkty.Add(produkt);

        string filePath = defaultFilePath;
        Xml.ExportToXml(produkty, filePath);
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
                Console.WriteLine($"{produkt.Ucinek} účinek: {produkt.Nazev}.");
            }
        }
        else
        {
            Console.WriteLine($"Filtru {produkt.Ucinek} účinek neodpovídá žádný produkt.");
        }
    }

    static void SkontrolujExpiraci()
    {
        DateTime dnes = DateTime.Today;
        List<Skincare> produktyKeSmazani = new List<Skincare>();
        bool nalezenExpirovany = false;

        foreach (var produkt in produkty)
        {
            if (produkt.DatumOtevreni.AddMonths(12) < dnes)
            {
                Console.WriteLine($"Produkt '{produkt.Nazev}' je expirovaný!");
                nalezenExpirovany = true;

                Console.Write("Přejete si tento produkt vymazat? (A/N): ");
                var odpoved = Console.ReadLine().Trim().ToUpper();

                if (odpoved == "A")
                {
                    produktyKeSmazani.Add(produkt);
                }
            }
        }

        if (produktyKeSmazani.Count > 0)
        {
            foreach (var produkt in produktyKeSmazani)
            {
                produkty.Remove(produkt);
            }

            UlozAUkonci(); 
        }
        else  
        {
            Console.WriteLine("Nejsou nalezeny žádné produkty s blížící se nebo prošlou expirací.");
        }
        
    }

    static void ZobrazProdukty()
    {
        foreach (var produkt in produkty)
        {
           if (produkty.Count > 0)
           {
                Console.WriteLine($"název: {produkt.Nazev}, účinek: {produkt.Ucinek},datum otevření: {produkt.DatumOtevreni}.");
           }
           else
           {
                Console.WriteLine("Produkt nenalezen.");
           }
        }
    }

    static void VymazProdukty()
    {
        Console.WriteLine("Zadejte název produktu.");
        string nazevKeSmazani = Console.ReadLine();

        Skincare produktKeSmazani = produkty.FirstOrDefault(p => p.Nazev.Equals(nazevKeSmazani, StringComparison.OrdinalIgnoreCase));

        if (produktKeSmazani != null)
        {
            produkty.Remove(produktKeSmazani);
            Console.WriteLine($"Produkt '{nazevKeSmazani}' byl úspěšně vymazán.");

            UlozAUkonci();
        }
        else
        {
            Console.WriteLine($"Produkt s názvem '{nazevKeSmazani}' nebyl nalezen.");
        }
    }

    static void UlozAUkonci()
    {
        string filePath = defaultFilePath;
        Xml.ExportToXml(produkty, filePath);
        
    }




    
}
