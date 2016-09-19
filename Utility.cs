using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Diagnostics;

namespace CatboobGGStream
{
    public static class Utility
    {
        public static ImageSource GetImageFromAppIcon(String app_path_p)
        {
            if (!File.Exists(app_path_p))
                return null;

            // Get the icon from the exicutable.
            System.Drawing.Icon temp_app_icon = System.Drawing.Icon.ExtractAssociatedIcon(app_path_p);

            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(temp_app_icon.Handle, new Int32Rect(0, 0, temp_app_icon.Width, temp_app_icon.Height), BitmapSizeOptions.FromEmptyOptions());
        }

        public static void ExecuteCommand(String command, String arguments)
        {
            if (!File.Exists(command))
                return;

            // Get the info for the cmd to run.
            ProcessStartInfo startInfo = new ProcessStartInfo(command, arguments);
            startInfo.UseShellExecute = true;
            try
            {
                // Bug#9: Some progams need to reference there starting directory. Try to assign the working directory.
                startInfo.WorkingDirectory = Path.GetDirectoryName(command);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Error: Invalid path entered! {0}", e.Message));
            }
            startInfo.Verb = "runas";

            // Start the cmd.
            Process command_to_execute= new Process();
            command_to_execute.StartInfo = startInfo;
            command_to_execute.Start();
        }
    }
}
