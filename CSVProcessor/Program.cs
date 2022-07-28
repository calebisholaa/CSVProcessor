using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVProcessor
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Reader rd = new Reader();

            rd.Run();

            Console.ReadLine();


        }
    }
}
