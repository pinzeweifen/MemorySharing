using System;
using System.Collections.Generic;
using System.IO;

namespace Memory
{
    public class MeoryListen
    {
        string project;
        FileSystemWatcher watcher;
        Dictionary<string, List<Action>> dic = new Dictionary<string, List<Action>>();

        public MeoryListen(string project)
        {
            this.project = project;
            watcher = new FileSystemWatcher();
            var dir = MemoryPath.RootPath + project;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            watcher.Path = dir;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += FileSystemEventHandler;
        }

        ~MeoryListen()
        {
            watcher.Changed -= FileSystemEventHandler;
            watcher.Dispose();
            watcher = null;
        }

        public void Register(string key, Action action)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, new List<Action>());
            }
            dic[key].Add(action);
        }

        public void Logout(string key, Action action)
        {
            if (!dic.ContainsKey(key)) return;
            var index = dic[key].IndexOf(action);
            if (index == -1) return;
            dic[key].RemoveAt(index);
            if (dic[key].Count == 0)
            {
                dic.Remove(key);
            }
        }

        void FileSystemEventHandler(object sender, FileSystemEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            watcher.EnableRaisingEvents = true;
            foreach (var key in dic.Keys)
            {
                if (key == e.Name)
                {
                    var count = dic[key].Count;
                    for (int i = 0; i < count; i++)
                    {
                        dic[key][i]();
                    }
                    break;
                }
            }
        }

    }
}
