#nullable enable
using System.Management;

namespace HPIndigoSysValTool.SystemInfo.Windows;

public class WmiProperty
{
    private readonly PropertyData data;

    public WmiProperty(PropertyData p)
    {
        data = p;
    }

    public string Name => data.Name;

    public object? Value => data.Cim2SystemValue();

    public Type SystemType => data.Cim2SystemType();

    public void Dump(bool skipNull)
    {
        if (Value == null)
        {
            if (skipNull)
                return;
            Console.WriteLine(Name + ": null");
        }
        else
        {
            Console.WriteLine("{0,-30}: {1} [{2}] ", Name, Value, SystemType);
        }
    }
}