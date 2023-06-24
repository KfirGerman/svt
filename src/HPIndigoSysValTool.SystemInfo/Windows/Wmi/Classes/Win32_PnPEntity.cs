#nullable enable
using System.Management;

namespace HPIndigoSysValTool.SystemInfo.Windows;

public class Win32_PnPEntity
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Win32_PnPEntity_Instance> GetInstances()
    {
        return GetInstances("SELECT * FROM Win32_PnPEntity");
    }

    /// <summary>
    /// </summary>
    /// <param name="wql"></param>
    /// <returns></returns>
    public static IEnumerable<Win32_PnPEntity_Instance> GetInstances(string wql)
    {
        using (var searcher = new ManagementObjectSearcher(GetCIMV2Scope(), new ObjectQuery(wql)))
        {
            foreach (ManagementObject mo in searcher.Get())
                if (mo != null)
                    yield return new Win32_PnPEntity_Instance(mo);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="deviceID"></param>
    /// <returns></returns>
    public static IEnumerable<Win32_PnPEntity_Instance> GetInstancesByID(string deviceID)
    {
        using (var searcher = new ManagementObjectSearcher(GetCIMV2Scope(), new ObjectQuery(
                   "SELECT * FROM Win32_PnPEntity WHERE (DeviceID = '" + FixDeviceID(deviceID) + "')")))
        {
            foreach (ManagementObject mo in searcher.Get())
                if (mo != null)
                    yield return new Win32_PnPEntity_Instance(mo);
        }
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static ManagementScope GetCIMV2Scope()
    {
        var cimV2Scope = new ManagementScope("\\\\" + "localhost" + "\\root\\CIMV2", null);
        cimV2Scope.Connect();
        return cimV2Scope;
    }

    /// <summary>
    /// </summary>
    /// <param name="deviceID"></param>
    /// <returns></returns>
    private static string FixDeviceID(string deviceID)
    {
        return !deviceID.Contains("\\\\") ? deviceID.Replace("\\", "\\\\") : deviceID;
    }
}