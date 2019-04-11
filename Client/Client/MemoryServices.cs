using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Memory
{
    public class MemoryServices : Memory
    {
        public MemoryServices(string project) : base(project)
        {
            MemorySharingClear.Add(project);
        }

        public void Write(string key, object value)
        {
            string path = string.Format(format, MemoryPath.RootPath, project, key);
            lock (_lock)
            {
                try
                {
                    while (FileIsUsed(path)) ;
                    var info = new FileInfo(path);
                    if (!info.Directory.Exists)
                        info.Directory.Create();
                    using (FileStream fs = File.Create(path))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, value);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
