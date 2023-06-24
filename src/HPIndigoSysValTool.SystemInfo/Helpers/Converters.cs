using System.Globalization;
using ByteSizeLib;

namespace HPIndigoSysValTool.SystemInfo.Helpers;

public class Converters
{
    /// <summary>
    ///     Convert bytes to MB, GB, TB, PB, EB, ZB, YB
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string GetByteSizeString(ulong sizeInBytes)
    {
        try
        {
            var sizeUnits = new Dictionary<string, double>
            {
                ["GB"] = Math.Pow(1024, 3),
                ["MB"] = Math.Pow(1024, 2),
                ["KB"] = 1024,
                ["B"] = 1
            };

            if (sizeInBytes == 0) return "0 B";

            var byteSize = ByteSize.FromBytes(sizeInBytes);
            var unit = sizeUnits.First(kvp => byteSize.Bytes >= kvp.Value).Key;
            var size = byteSize.Bytes / sizeUnits[unit];
            return $"{size:0.00} {unit}";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while converting bytes to a size string.", ex);
        }
    }

    /// <summary>
    ///     Convert DateTime string to a readable format "dd-MM-yyyy hh:mm:ss"
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string ConvertDateTimeString(string input)
    {
        const string DATE_FORMAT = "yyyyMMdd";
        const string TIME_FORMAT = "hhmmss";
        const string OUTPUT_FORMAT = "dd-MM-yyyy hh:mm:ss";

        if (string.IsNullOrEmpty(input))
            //throw new ArgumentException("Input string cannot be null or empty.", nameof(input));
            return input;

        if (input.Length < 14)
            //throw new FormatException("Input string has an invalid length.");
            return input;

        try
        {
            // Split the input string into the date and time parts
            var datePart = input.Substring(0, 8);
            var timePart = input.Substring(8, 6);

            // Parse the date and time parts into DateTime objects
            var date = DateTime.ParseExact(datePart, DATE_FORMAT, CultureInfo.InvariantCulture);
            var time = TimeSpan.ParseExact(timePart, TIME_FORMAT, CultureInfo.InvariantCulture);

            // Combine the date and time into a single DateTime object
            var datetime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);

            // Return the formatted string
            return datetime.ToString(OUTPUT_FORMAT);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while converting DateTime string.", ex);
        }
    }
}