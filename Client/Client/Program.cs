using System;
using Memory;

[Serializable]
class A
{
    public int a;
}

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var ms = new MemoryClient("q666");
            Console.WriteLine(((A)ms.Read("q")).a);


            var mss = new MemoryServices("zzzz");
            mss.Write("z", "我是测试");

            Console.ReadLine();

        }
    }
}
