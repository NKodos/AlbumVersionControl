using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AlbumVersionControl.Configs;
using AlbumVersionControl.Models;

namespace AlbumVersionControl
{
    public static class Generator
    {
        public static void Generate()
        {

            var generatedClassesFolder = new AppConfiguration().GeneratedClassesFolder;
            var xsdFiles = Directory.GetFiles(generatedClassesFolder, "*.xsd").ToList();
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