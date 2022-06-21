using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberParser
{
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

            int[] intArray = numbers.Split(',').Select(x => int.Parse(x)).ToArray();

            Array.Sort(intArray);

            Array.Reverse(intArray);

            Console.Write(string.Join(",", intArray) + " " + file);
            Console.ReadLine();
        }
    }
}
