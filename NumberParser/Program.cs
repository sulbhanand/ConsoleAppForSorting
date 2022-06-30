using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;

namespace NumberParser
{
    public class JSONOBJECT
    {
        public int[] SortArray { get; set; }
    }
    public interface IFactory
    {
        string SortStringInDescending(string numbers);
    }

    public class XMLOUTPUT : IFactory
    {
        public string SortStringInDescending(string numbers)
        {
            string xmlReturn = string.Empty;
            string path = "D:\\Test\\DesignWayTest\\ConsoleAppForSorting\\NumberParser\\Output\\NumberParser.xml";
            int[] intArray = numbers.Split(',').Select(x => int.Parse(x)).ToArray();

            Array.Sort(intArray);

            Array.Reverse(intArray);

            using (XmlWriter writer = XmlWriter.Create(path))
            {
                writer.WriteStartElement("Sort");
                foreach (int item in intArray)
                {
                    writer.WriteElementString("Number", item.ToString());
                }
                writer.WriteEndElement();
                writer.Flush();

            }
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            xmlReturn = doc.InnerXml;
            return xmlReturn;
        }
    }

    public class JSONOUTPUT : IFactory
    {
        public string SortStringInDescending(string numbers)
        {
            string path = "D:\\Test\\DesignWayTest\\ConsoleAppForSorting\\NumberParser\\Output\\NumberParser.json";

            int[] intArray = numbers.Split(',').Select(x => int.Parse(x)).ToArray();

            Array.Sort(intArray);

            Array.Reverse(intArray);

            string json = JsonConvert.SerializeObject(intArray);
            File.WriteAllText(path, json);

            return JsonConvert.SerializeObject(string.Join(",", intArray).Split(','));
        }
    }

    public class TXTOUTPUT : IFactory
    {
        public string SortStringInDescending(string numbers)
        {
            string path = "D:\\Test\\DesignWayTest\\ConsoleAppForSorting\\NumberParser\\Output\\NumberParser.txt";
            
            int[] intArray = numbers.Split(',').Select(x => int.Parse(x)).ToArray();

            Array.Sort(intArray);

            Array.Reverse(intArray);


            if (!File.Exists(path))
                File.Create(path).Dispose();

             using (TextWriter tw = new StreamWriter(path))
             {
                tw.WriteLine(string.Join(",", intArray));
             }

            return string.Join(",", intArray);
        }
    }

    public abstract class Factory
    {
        public abstract IFactory FactoryMethod(string type);
    }

    public class ConcreteFactory : Factory
    {
        public override IFactory FactoryMethod(string type)
        {
            switch (type)
            {
                case "XML": return new XMLOUTPUT();
                case "JSON": return new JSONOUTPUT();
                case "TXT": return new TXTOUTPUT();
                default: throw new ArgumentException("Invalid file format", "type");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string numberFile;
            Console.Write("Enter the number for sorting and file name following with space e.g.(4,5,1,9,10,58,34,12,0 <space> XML):\n");
            numberFile = Console.ReadLine();

            Console.WriteLine("\n Your Input is {0}: \n", numberFile);

            string numbers = numberFile.Split(' ')[0];

            string file = numberFile.Split(' ')[numberFile.Split(' ').Length - 1];

            Factory factory = new ConcreteFactory();
            if (file == "TXT")
            {
                IFactory xmlOut = factory.FactoryMethod("TXT");
                xmlOut.SortStringInDescending(numbers);
            }
            else if (file == "JSON")
            {
                IFactory jsonOut = factory.FactoryMethod("JSON");
                Console.WriteLine(jsonOut.SortStringInDescending(numbers));
                Console.ReadLine();
            }
            else if (file == "XML")
            {
                IFactory xmlOut = factory.FactoryMethod("XML");
                Console.WriteLine(xmlOut.SortStringInDescending(numbers));
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please Enter the vaild file format");
                Console.ReadLine();
            }

        }
    }
}
