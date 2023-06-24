#nullable enable
using System.Globalization;
using System.Management;
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public static class CimConverter
{
    private static readonly IDictionary<CimType, Type> Cim2TypeTable =
        new Dictionary<CimType, Type>
        {
            {
                CimType.Boolean,
                typeof(bool)
            },
            {
                CimType.Char16,
                typeof(string)
            },
            {
                CimType.DateTime,
                typeof(DateTime)
            },
            {
                CimType.Object,
                typeof(object)
            },
            {
                CimType.Real32,
                typeof(float)
            },
            {
                CimType.Real64,
                typeof(double)
            },
            {
                CimType.Reference,
                typeof(object)
            },
            {
                CimType.SInt16,
                typeof(short)
            },
            {
                CimType.SInt32,
                typeof(int)
            },
            {
                CimType.SInt64,
                typeof(long)
            },
            {
                CimType.SInt8,
                typeof(sbyte)
            },
            {
                CimType.String,
                typeof(string)
            },
            {
                CimType.UInt8,
                typeof(byte)
            },
            {
                CimType.UInt16,
                typeof(ushort)
            },
            {
                CimType.UInt32,
                typeof(uint)
            },
            {
                CimType.UInt64,
                typeof(ulong)
            }
        };

    public static Type Cim2SystemType(this PropertyData data)
    {
        var type = Cim2TypeTable[data.Type];
        if (data.IsArray)
            type = type.MakeArrayType();
        return type;
    }

    public static object? Cim2SystemValue(this PropertyData data)
    {
        if (data.Value == null)
            return null;
        var conversionType = data.Cim2SystemType();
        return data.Type == CimType.DateTime
            ? DateTime.ParseExact(data.Value.ToString(), "yyyyMMddHHmmss.ffffff",
                CultureInfo.InvariantCulture)
            : Convert.ChangeType(data.Value, conversionType);
    }
}