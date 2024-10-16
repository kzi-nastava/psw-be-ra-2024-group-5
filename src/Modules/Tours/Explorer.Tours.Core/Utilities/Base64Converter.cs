using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Utilities {
    public static class Base64Converter {

        public static byte[] ConvertToByteArray(string base64Image) {
            return string.IsNullOrEmpty(base64Image) ? null : Convert.FromBase64String(base64Image);
        }

        public static string ConvertFromByteArray(byte[] image) {
            return image == null ? null : Convert.ToBase64String(image);
        }
    }
}
