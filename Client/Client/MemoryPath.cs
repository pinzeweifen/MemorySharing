using System;
using System.IO;
using System.Management;

namespace Memory
{
    static class MemoryPath
    {
        static string _rootPath;
        public static string RootPath
        {
            get
            {
                return _rootPath ?? (_rootPath = GetSystemDisk() + @"\MemorySharing\");
            }
        }

        static string GetSystemDisk()
        {
            try
            {
                SelectQuery selectQuery = new SelectQuery("select * from win32_logicaldisk");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(selectQuery);
                ManagementObjectCollection diskcollection = searcher.Get();
                if (diskcollection != null && diskcollection.Count > 0)
                {
                    foreach (ManagementObject disk in searcher.Get())
                    {
                        int nType = Convert.ToInt32(disk["DriveType"]);
                        if (nType != Convert.ToInt32(DriveType.Fixed))
                        {
                            continue;
                        }
                        else
                        {
                            var id = disk["DeviceID"].ToString();
                            if (Environment.GetEnvironmentVariable("windir").Remove(2) == id)
                            {
                                return id;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }
    }
}
