using System;
using System.Collections.Generic;
using System.Text;

namespace SaveImageToN8ZAK
{
 class Program
 {
  static private bool success;
  static private System.Net.WebClient webClient;
  static private Chilkat.SFtp sftp;

  static void Main(string[] args)
  {
   while (true)
   {
    for (int i = 1; i <= 4; i++)
    {
     Console.Write(System.DateTime.Now.ToString("hh:mm:ss") +  " Saving Camera " + System.Configuration.ConfigurationManager.AppSettings.Get("CameraIPAddress") + " " + i);
     webClient = new System.Net.WebClient();
     try
     {
          webClient.DownloadFile("http://" + System.Configuration.ConfigurationManager.AppSettings.Get("CameraIPAddress").ToString() + "/stillimg" + i + ".jpg", "stillimg" + i + ".jpg");
     }
     catch
     {
        //System.IO.File.Copy("offline.jpg","stillimg" + i + ".jpg", true);
            try
            {
                    //Modify file and add date time - so user knows prog is working
                    System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile("offline.jpg");
                    System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
                    gr.DrawString(System.DateTime.Now.ToString(),new System.Drawing.Font("Arial",24),System.Drawing.Brushes.Black,new System.Drawing.Point(175,255));
                    bmp.Save("stillimg" + i + ".jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
                    bmp.Dispose();
                    bmp = null;
            }
            catch (System.Exception ex1)
            {
                Console.WriteLine("ERROR " + ex1.Message);
            }
     }
     Console.WriteLine("...Done.");
     webClient.Dispose();
     webClient = null;
    }

    Console.WriteLine("Images Saved...Trying to Startup Chilkat and Upload");
    try
    {
     sftp = new Chilkat.SFtp();
     Console.WriteLine("Chilkat Loaded");
     success = sftp.UnlockComponent("------------------MUST ENTER THIS STRING-------------------------");
     Console.WriteLine("Chilkat Unlocked");
     if (success)
     {
      //  Set some timeouts, in milliseconds:
      sftp.ConnectTimeoutMs = 5000;
      sftp.IdleTimeoutMs = 10000;

      success = sftp.Connect("------------WEBSITE-------------", -----------PORT-------------);
      if (success)
      {
       success = sftp.AuthenticatePw("-----------USERNAME--------------", "---------PASSWORD----------");
       if (success)
       {
        //  After authenticating, the SFTP subsystem must be initialized:
        success = sftp.InitializeSftp();
        if (success)
        {
         for (int j = 1; j <= 4; j++)
         {
          Console.Write(System.DateTime.Now.ToString("hh:mm:ss") + "Uploading to ----------------WEBSITE--------------...Camera " + j + "...");
          string handle;
          handle = sftp.OpenFile("-----------PATH TO SAVE STATIC IMAGES--------------/stillimg" + j + ".jpg", "writeOnly", "createTruncate");
          if (handle != null)
          {
           //  Upload from the local file to the SSH server.
           success = sftp.UploadFile(handle, "stillimg" + j + ".jpg");
           if (success)
           {
            //  Close the file.
            success = sftp.CloseHandle(handle);
            if (success)
            {
             Console.WriteLine("Done!");
            }
            else
            {
             Console.WriteLine("ERROR!");
            }
           }
           else
           {
            Console.WriteLine("ERROR!");
           }
          }
          else
          {
           Console.WriteLine("ERROR!");
          }
          handle = null;
         }//endfor
        }
        else
        {
         Console.WriteLine("ERROR!");
        }
       }
       else
       {
        Console.WriteLine("ERROR!");
       }
      }
      else
      {
       Console.WriteLine("ERROR!");
      }
     }
     else
     {
      Console.WriteLine("ERROR!");
     }
    }
    catch (System.Exception ex)
    {
     Console.WriteLine("ERROR - " + ex.Message);
    }
    int sleeptime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("SleepTime"));
    Console.WriteLine("Sleeping for " + sleeptime + " milliseconds");
    System.Threading.Thread.Sleep(sleeptime);
   }
  }
 }
}
