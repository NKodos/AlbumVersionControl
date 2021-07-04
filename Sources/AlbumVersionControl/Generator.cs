using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlbumVersionControl.Configs;

namespace AlbumVersionControl
{
    public static class Generator
    {
        public static void Generate()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();

                if (result != DialogResult.OK) return;
                var generatedClassesFolder = dialog.SelectedPath;

                var xsdFiles = Directory.GetFiles(new AppConfiguration().GeneratedClassesFolder, "*.xsd").ToList();
                var fileArguments = xsdFiles.Aggregate("", (current, xsdFile) => current + xsdFile + " ");
                var commandLine = $"{fileArguments} /classes /o:{generatedClassesFolder}";

                var p = new ProcessStartInfo("xsd.exe")
                {
                    Arguments = commandLine, WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                };
                Process.Start(p);
            }
        }
    }
}