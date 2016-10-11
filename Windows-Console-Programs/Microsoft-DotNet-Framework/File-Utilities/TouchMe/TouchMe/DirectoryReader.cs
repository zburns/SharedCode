using System;
using System.Collections.Generic;
using System.Text;

namespace TouchMe
{
    class DirectoryReader : System.Collections.ArrayList
    {
        bool m_recursive;
        System.Text.RegularExpressions.Regex m_selectionPattern;

        public DirectoryReader(bool isRecursive, string selectionPattern)
        {
            this.m_recursive = isRecursive;
            this.m_selectionPattern = new System.Text.RegularExpressions.Regex("^" + System.Text.RegularExpressions.Regex.Escape(selectionPattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$");
        }

        public void collectFiles(string path)
        {
            Console.WriteLine("Collecting files for " + path + ". Please wait...\n");
            string[] files = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                if (this.isEligible(files[i]))
                {
                    this.Add(files[i]);
                }
            }
            if (this.m_recursive)
            {
                string[] directories = System.IO.Directory.GetDirectories(path);
                for (int j = 0; j < directories.Length; j++)
                {
                    this.collectFiles(directories[j]);
                }
            }
        }


        private bool isEligible(string filename)
        {
            return this.m_selectionPattern.IsMatch(filename);
        }

 

 

    }
}
