using System;
using System.Collections.Generic;
using System.Text;

namespace TouchMe
{
    class Touch
    {
        string m_pattern;
        string m_root;
        bool m_subdirs;

        public Touch(string root, string pattern, bool subdirs)
        {
            this.m_root = root;
            this.m_pattern = pattern;
            this.m_subdirs = subdirs;
        }

        public void Go(DateTime when)
        {
            DirectoryReader reader = new DirectoryReader(this.m_subdirs, this.m_pattern);
            reader.collectFiles(this.m_root);
            foreach (string str in reader)
            {
                this.touchFile(str, when);
            }
        }

        public void touchFile(string filename, DateTime when)
        {
            string str = "";
            try
            {
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.SetLastWriteTime(filename, when);
                    str = "SET: " + filename;
                }
                else
                {
                    str = string.Format("\nERROR: Cannot find file " + filename + "\n", new object[0]);
                }
            }
            catch (Exception exception)
            {
                str = string.Format("\nERROR: Cannot touch file {0}: {1}\n", str, exception.Message);
            }
            Console.WriteLine(str);
        }
    }
}
