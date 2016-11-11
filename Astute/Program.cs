using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astute
{
    class Program
    {
        public static void Main(string[] args)
        {
            Input.TcpInput.Subscribe(Console.WriteLine);
        }
    }
}
