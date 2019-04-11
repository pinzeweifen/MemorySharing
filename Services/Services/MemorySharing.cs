using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Memory
{
    public class MemoryClient : Memory
    {
        public MemoryClient(string project) : base(project)
        {
        }

        public object Read(string key)
        {
            string path = string.Format(format, MemoryPath.RootPath, project, key);
            lock (_lock)
            {
                try
                {
                    if (!File.Exists(path)) return null;
                    while (FileIsUsed(path)) ;
                    using (FileStream fs = File.Open(path, FileMode.Open))
                    {
                        IFormatter bf = new BinaryFormatter();
                        bf.Binder = new MemorySerializationBinder();
                        return bf.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
        }
    }
}
