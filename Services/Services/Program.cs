using Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[Serializable]
class A
{
    public int a;
}

namespace Services
{
    class Program
    {
        static void Main(string[] args)
        {
            var ms = new MemoryServices("q666");
            ms.Write("q", new A { a = 6666 });


            var ml = new MeoryListen("zzzz");
            ml.Register("z", () =>
            {
                var msc = new MemoryClient("zzzz");
                Console.WriteLine(msc.Read("z"));
            });
            Console.Read();

        }
    }
}
