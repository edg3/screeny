/// <summary>
/// 
///     Built by: Ernest "edg3" Loveland
///     Description:
///         This is an "application" that can be set to run
///         on a key (or mouse press) - it make a screenshot.
///         
///         Was initially for screenshot's in Minecraft,
///         hence making it work for full screen games. It
///         also then got the extra version for simple
///         additions.
///     
///     What this does:
///      - takes a screenshot of the screen
///      - allows a file to be made for an overlay
///     
///     What to do:
///         - Create a shortcut to "Screeny.exe"
///         - Right-click it and go to properties
///         - Click "Shortcut Key"
///         - Press the keys for the shortcut
///         - Click "Apply" then "ok"
///         [You can now use "Screeny"]
///         
///     To add an "overlay" is simple. Just create an
///     "overlay.png" next to the EXE, make shortcut, etc.
///     It will make the setup for you, it is easy and simple
///     to understand.
///     
///     As an added note: you can make a "shortcut" binding
///     on your mouse button (tested with Razer Imperator
///     click mid button) but they will be in Razer's folder
///     unless you create a "setup.cfg" (see code below).
///     It will be added to the readme eventually.
/// 
/// </summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screeny
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            /// <summary>
            /// Load a config file
            /// </summary>
            string FileToSave = "";
            if (File.Exists("setup.cfg"))
            {
                try
                {
                    var setupFile = File.ReadAllText("setup.cfg");
                    var splitSetupFile = setupFile.Split('=');
                    if (splitSetupFile[0] == "folder")
                    {
                        FileToSave = splitSetupFile[1];
                    }
                }
                catch
                {
                    throw new Exception("setup.cfg file not correct. folder={where} only currently");
                }
            }


            /// <summary>
            /// Capture the screen display.
            /// </summary>
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Graphics Graphics1;
            Bitmap Bitmap1 = new Bitmap(screenWidth, screenHeight);
            Graphics1 = Graphics.FromImage(Bitmap1);
            Graphics1.CopyFromScreen(Point.Empty, Point.Empty, Screen.PrimaryScreen.Bounds.Size);

            DateTime ImageDate = DateTime.Now;
            string SaveTime = ImageDate.Year.ToString() + "-" + ImageDate.Month.ToString() + "-" + ImageDate.Day.ToString() + "." +
                         ImageDate.Hour.ToString() + "_" + ImageDate.Minute.ToString() + "_" + ImageDate.Millisecond.ToString();

            /// <summary>
            /// Make the image an overlay
            /// </summary>
            if (File.Exists(FileToSave + "overlay.png"))
            {
                Bitmap Bitmap2 = (Bitmap)Bitmap.FromFile(FileToSave + "overlay.png");

                var target = new Bitmap(Bitmap1.Width, Bitmap1.Height, PixelFormat.Format32bppArgb);
                var graphics = Graphics.FromImage(target);
                graphics.CompositingMode = CompositingMode.SourceOver; // this is the default, but just to be clear

                graphics.DrawImage(Bitmap1, 0, 0);
                graphics.DrawImage(Bitmap2, 0, 0);

                target.Save(FileToSave + SaveTime + "_overlayed.png", ImageFormat.Png);
            }

            /// <summary>
            /// Save the image in a bitmap.
            /// </summary>
            Bitmap1.Save(FileToSave + SaveTime + ".png"); //Place that you want to save screenshot

        }
    }
}
