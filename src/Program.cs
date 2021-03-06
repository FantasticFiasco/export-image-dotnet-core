﻿using System;
using System.IO;

namespace ExportImage
{
    class Program
    {
        static void Main(string[] imagePaths)
        {
            Console.WriteLine($"Exporting {imagePaths.Length} images...");

            var settings = Settings.Load();
            var encoder = new Encoder(settings.Quality);

            for (var index = 0; index < imagePaths.Length; index++)
            {
                var imagePath = imagePaths[index];
                var (exportDirectoryPath, exportPath) = GetExportPaths(imagePath, settings.SubDirectory);

                EnsureDirectoryExists(exportDirectoryPath);

                Console.WriteLine($"{index + 1}/{imagePaths.Length}\t-> {exportPath}");

                encoder.SaveImage(imagePath, exportPath);
            }

            Console.WriteLine("Done!");

            if (settings.PreventTermination)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private static (string, string) GetExportPaths(string imagePath, string subDirectory)
        {
            var directoryPath = Path.GetDirectoryName(imagePath);
            var name = Path.GetFileName(imagePath);

            var exportDirectoryPath = Path.Combine(directoryPath, subDirectory);
            var exportPath = Path.Combine(exportDirectoryPath, name);

            return (exportDirectoryPath, exportPath);
        }

        private static void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
