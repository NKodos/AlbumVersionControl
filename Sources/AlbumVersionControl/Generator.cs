using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AlbumVersionControl
{
    public static class Generator
    {
        public const string FolderPath = @"C:\Users\User\Desktop\VersionContent";

        public static void Generate()
        {
            var xsdFiles = Directory.GetFiles(FolderPath, "*.xsd")
                .ToList();

            var fileArguments = xsdFiles.Aggregate("", (current, xsdFile) => current + (Path.GetFileName(xsdFile) + " "));

            var commandLine = $"/C ..\\xsd {fileArguments} /classes";

            var p = new ProcessStartInfo("cmd.exe")
            {
                Arguments = commandLine, WorkingDirectory = FolderPath
            };
            Process.Start(p);
        }
    }
}