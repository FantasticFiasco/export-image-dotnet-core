﻿using System;
using System.IO;
using SixLabors.ImageSharp;

namespace ExportImage
{
    class Program
    {
        static void Main(string[] imagePaths)
        {
            Console.WriteLine($"Exporting {imagePaths.Length} images...");

            for (var index = 0; index < imagePaths.Length; index++)
            {
                var imagePath = imagePaths[index];
                var imageName = Path.GetFileName(imagePath);
                var directoryPath = Path.GetDirectoryName(imagePath);

                var newDirectoryPath = Path.Combine(directoryPath, "Exported");
                var newImagePath = Path.Combine(newDirectoryPath, imageName);

                if (!Directory.Exists(newDirectoryPath))
                {
                    Directory.CreateDirectory(newDirectoryPath);
                }

                using (var image = Image.Load(imagePath))
                using (var writer = File.Create(newImagePath))
                {
                    Console.WriteLine($"{index + 1}/{imagePaths.Length}\t-> {newImagePath}");

                    image.SaveAsJpeg(writer);
                }
            }

            Console.WriteLine("Done!");
        }
    }
}
