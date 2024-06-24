using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace pF
{
    public static class Xml
    { 
    public static void ExportToXml(List<Skincare> produkty, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Skincare>));
        using TextWriter writer = new StreamWriter(filePath);
        serializer.Serialize(writer, produkty);
    }

    public static List<Skincare> ImportFromXml(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Skincare>));
        using (TextReader reader = new StreamReader(filePath))
        {
            return (List<Skincare>)serializer.Deserialize(reader);
        }
    }

    }
}
