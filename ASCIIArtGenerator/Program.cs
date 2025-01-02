using System;
using System.Text;
using System.Drawing;

Console.WriteLine("Enter the path to the image file:");

string imagePath = Console.ReadLine();

//Load the image

try
{
    using (Bitmap bitmap = new Bitmap(imagePath))
    {
        // Resize the image for better console output
        Bitmap resizedBitmap = ResizeImage(bitmap, 100, 50);

        // Convert to ASCII art
        string asciiArt = ConvertToASCII(resizedBitmap);
        Console.WriteLine(asciiArt);

        // Optional: Save to a file
        System.IO.File.WriteAllText("ascii_art.txt", asciiArt);
        Console.WriteLine("\nASCII art saved to ascii_art.txt");
    }
} catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

static Bitmap ResizeImage(Bitmap original, int width, int height)
{
    // Resize the image while maintaining aspect ratio
    Bitmap resized = new Bitmap(width, height);
    using (Graphics g = Graphics.FromImage(resized))
    {
        g.DrawImage(original, 0, 0, width, height);
    }
    return resized;
}

static string ConvertToASCII(Bitmap bitmap)
{
    // Define ASCII character for brightness levels
    string asciiChars = "@%#*+=-:. ";
    StringBuilder asciiArt = new StringBuilder();

    for (int y = 0; y < bitmap.Height; y++)
    {
        for (int x = 0; x < bitmap.Width; x++)
        {
            // Get pixel color
            Color pixelColor = bitmap.GetPixel(x, y);

            // Calculate brightness (0 to 255)
            int brightness = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

            // Map brightness to an ascii character 
            int index = (brightness * (asciiChars.Length - 1)) / 255;
            asciiArt.Append(asciiChars[index]);
        }
        asciiArt.AppendLine();
    }

    return asciiArt.ToString();
}