using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Utilities
{
    public static class Base64Converter
    {
        public static byte[] ConvertToByteArray(string? base64Image)
        {
            try
            {
                return Convert.FromBase64String(base64Image);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid Base64 string: " + ex.Message);
                return null;
            }
        }

        public static string ConvertFromByteArray(byte[]? image)
        {
            return image == null ? null : Convert.ToBase64String(image);
        }
    }
}
