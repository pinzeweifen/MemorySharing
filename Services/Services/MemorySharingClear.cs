using System;
using System.IO;
using System.Collections.Generic;

namespace Memory
{
    public class MemorySharingClear
    {
        static bool isInit = false;
        static List<string> list = new List<string>();
        public static void Add(string pro)
        {
            if (!isInit)
            {
                AppDomain.CurrentDomain.ProcessExit += ProcessExit;
                isInit = true;
            }

            var path = MemoryPath.RootPath + pro;
            if (list.IndexOf(path) == -1)
            {
                list.Add(path);
            }
            
        }

        static void ProcessExit(object sender, EventArgs e)
        {
            foreach(var tmp in list)
            {
                if(Directory.Exists(tmp))
                Directory.Delete(tmp,true);
            }
        }
    }
}
