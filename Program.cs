// See https://aka.ms/new-console-template for more information
using pF;
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

        XmlSerializer serializer = new XmlSerializer(typeof(List<Skincare>));
        using TextWriter writer = new StreamWriter(defaultFilePath);
        serializer.Serialize(writer, produkty);

        

        while (true)
        {
            Console.WriteLine("1. Přidej produkt.");
            Console.WriteLine("2. Třiď podle účinku.");
            Console.WriteLine("3. Kontrola expirace.");
            Console.WriteLine("4. Seznam produktů.");
            Console.WriteLine("5. Konec.");
            Console.Write("Zvolte možnost.");

            Console.WriteLine();
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
        Skincare produkt = new Skincare();// preco to nejde ?????

        Console.WriteLine("Zadejte značku produktu: ");
        produkt.Znacka = Console.ReadLine();
        Console.WriteLine("Zadejte název produktu: ");
        produkt.Nazev = Console.ReadLine();
        Console.WriteLine("Účinek: ");
        produkt.Ucinek = Console.ReadLine();
        Console.WriteLine("Datum otevření: ");
        DateTime DatumOtevreni = DateTime.Parse(Console.ReadLine());

        produkty.Add(produkt);
        Console.WriteLine("Produkt přidán.");
    }

    static void TridPodleUcinku()
    {
        Console.WriteLine("Zadejte požadovaný efekt: ");
        string ucinek = Console.ReadLine();
        var vyfiltrovaneProdukty = produkty.Where(p => p.Ucinek.Equals(ucinek)).ToList();
        if (vyfiltrovaneProdukty.Count > 0)
        {
            foreach (var produkt in vyfiltrovaneProdukty)
            {
                Console.WriteLine($"Filtru odpovídá {produkt}.");
            }
        }
        else
        {
            Console.WriteLine("Filtru neodpovídá žádný produkt.");
        }
    }

    static void SkontrolujExpiraci()
    {
        foreach(var produkt in produkty)
        {
            DateTime DatumExpirace = produkt.DatumOtevreni.AddDays(365);
        }
    }

    static void ZobrazProdukty()
    {
        foreach (var produkt in produkty)
        {
            if (produkty.Count > 0)
            {
                Console.WriteLine($"{produkt.Znacka},{produkt.Ucinek},{produkt.DatumOtevreni}");
            }
            else
            {
                Console.WriteLine("Produkt nenalezen.");
            }

        }
    }

    static void UlozAUkonci()
    {
    
        XmlSerializer serializer = new XmlSerializer(typeof(List<Skincare>));
        using TextWriter writer = new StreamWriter(defaultFilePath);
        serializer.Serialize(writer, produkty);
        return;
    }




}
