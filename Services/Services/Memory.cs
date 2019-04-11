using System;
using System.IO;

namespace Memory
{
    public class Memory
    {
        protected readonly string format = @"{0}\{1}\{2}";
        protected static object _lock = new object();
        protected string project;

        public Memory(string project)
        {
            this.project = project;
        }

        ~Memory()
        {
            project = null;
        }

        protected bool FileIsUsed(string path)
        {
            if (!File.Exists(path)) return false;
            FileStream fileStream = null;
            try
            {
                fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream = null;
                }
            }
        }
    }
}
