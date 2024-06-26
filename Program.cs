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
        
        foreach(var produkt in produkty)
        {
            if(produkt.DatumOtevreni.AddMonths(12) < dnes)
            { 
                Console.WriteLine($"Expirace: {produkt}");
            }
            else
            {
                Console.WriteLine("Nejsou žádné produkty s blížící se expirací.");
            }
            
        }
        
    }

    static void ZobrazProdukty()
    {
        foreach (var produkt in produkty)
        {
           if (produkty.Count > 0)
           {
                Console.WriteLine($"nazev: {produkt.Nazev}, účinek: {produkt.Ucinek},datum otevření: {produkt.DatumOtevreni}.");
           }
           else
           {
                Console.WriteLine("Produkt nenalezen.");
           }

        }
    }

    static void UlozAUkonci()
    {
        string filePath = defaultFilePath;
        Xml.ExportToXml(produkty, filePath);
        
    }




    
}
