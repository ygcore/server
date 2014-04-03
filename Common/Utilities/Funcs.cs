using System;
using System.Globalization;
using System.Text;

namespace Common.Utilities
{
    public static class Funcs
    {
        private static readonly string[] Baths;

        static Funcs()
        {
            Baths = new string[256];
            for (int i = 0; i < 256; i++)
                Baths[i] = String.Format("{0:X2}", i);
        }

        public static string ToHex(this byte[] array)
        {
            StringBuilder builder = new StringBuilder(array.Length * 2);

            for (int i = 0; i < array.Length; i++)
                builder.Append(Baths[array[i]]);

            return builder.ToString();
        }

        public static byte[] ToBytes(this String hexString)
        {
            try
            {
                byte[] result = new byte[hexString.Length / 2];

                for (int index = 0; index < result.Length; index++)
                {
                    string byteValue = hexString.Substring(index * 2, 2);
                    result[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid hex string: {0}", hexString);
                throw;
            }
        }

        public static string FormatHex(this byte[] buffer)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("Buffer dump, length: {0}{1}Index   |---------------------------------------------|  |--------------|{1}", buffer.Length, Environment.NewLine);

            int index = 0, i;
            string hex, data;

            while (index < buffer.Length)
            {
                hex = data = String.Empty;

                for (i = 0; i < 16 && index + i < buffer.Length; i++)
                {
                    hex += buffer[index + i].ToString("x2") + " ";

                    if (buffer[i + index] > 31 && buffer[i + index] < 127)
                        data += (char)buffer[i + index];
                    else
                        data += ".";
                }

                while (i < 16)
                {
                    hex += "   ";
                    i++;
                }

                sb.AppendFormat("{0}   {1} {2}{3}", index.ToString("X5"), hex.ToUpper(), data, Environment.NewLine);
                index += 16;
            }

            sb.Append("        |---------------------------------------------|  |--------------|");

            return sb.ToString();
        }
    }
}
