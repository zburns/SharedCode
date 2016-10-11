using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GetFrameworkVersion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            try
            {
                System.Text.StringBuilder output = new System.Text.StringBuilder();
                output.AppendLine("GetFrameworkVersion by Zachary Burns.");
                output.AppendLine("Microsoft .NET Framework Details...");
                try
                {
                    output.AppendLine("Found " + getOSArchitectureLegacy() + "bit system.");
                }
                catch
                {
                }
                output.AppendLine("");
                output.AppendLine(GetRegistryValue(@"Software\Microsoft\.NET Framework\Policy\v1.0", "1.0\t"));
                output.AppendLine(GetRegistryValue(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v1.1.4322", "1.1\t"));
                output.AppendLine(GetRegistryValue(@"Software\Microsoft\NET Framework Setup\NDP\v2.0.50727", "2.0\t"));
                output.AppendLine(GetRegistryValue(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0", "3.0\t"));
                output.AppendLine(GetRegistryValue(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5", "3.5\t"));
                output.AppendLine(GetRegistryValue(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", "4.0 Full"));
                output.AppendLine(GetRegistryValue(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Client", "4.0 Client"));
                output.AppendLine("");


                MessageBox.Show(output.ToString());
            }
            catch
            {
            }
        }

        static string GetRegistryValue(string path, string frameworkVer)
        {
            Microsoft.Win32.RegistryKey localMachine = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey key2 = localMachine.OpenSubKey(path);
            string val = "";

            if (key2 == null)
            {
                val = ".NET " + frameworkVer + "\tNot Installed.";
            }
            else
            {
                try
                {
                    if (!frameworkVer.Contains("1.0"))
                    {
                        object obj2 = key2.GetValue("SP");
                        if ((key2.GetValue("Install") != null) && (key2.GetValue("Install").ToString() == "1"))
                        {
                            if (obj2 != null)
                            {
                                val = string.Concat(new object[] { ".NET ", frameworkVer, "\tSP Version: ", obj2, "" });
                            }
                            else
                            {
                                val = ".NET " + frameworkVer + "\tSP Version: None";
                            }
                        }
                    }
                    else if (localMachine.OpenSubKey(@"Software\Microsoft\Active Setup\Installed Components\{78705f0d-e8db-4b2d-8193-982bdda15ecd}") != null)
                    {
                        string str2 = key2.GetValue("Version").ToString();
                        str2 = str2.Substring(str2.LastIndexOf('.') + 1);
                        val = ".NET " + frameworkVer + "\tSP Version: " + str2 + ".";
                    }
                    else
                    {
                        val = ".NET " + frameworkVer + "\tSP Version: No.";
                    }
                }
                catch (Exception exception)
                {
                    val = "Error occurred for :" + path + exception.Message;
                }
            }
            return val;
        }

        private static int getOSArchitectureLegacy()
        {
            string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            if (!string.IsNullOrEmpty(environmentVariable) && (string.Compare(environmentVariable, 0, "x86", 0, 3, true) != 0))
            {
                return 0x40;
            }
            return 0x20;
        }
    }
}
