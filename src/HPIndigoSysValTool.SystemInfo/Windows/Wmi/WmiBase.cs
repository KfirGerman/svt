//#nullable enable
//using System.ComponentModel;
//using System.Management;
//using System.Runtime.Versioning;

//namespace HPIndigoSysValTool.SystemInfo.Windows;

//[SupportedOSPlatform("windows")]
//public class WmiBase : IDisposable
//{
//    protected ManagementObject? managementObject;

//    public virtual void Dispose()
//    {
//        managementObject?.Dispose();
//    }

//    protected static ManagementObjectCollection? GetWMIObject(string scope, string query)
//    {
//        using var searcher = new ManagementObjectSearcher(scope, query);
//        try
//        {
//            return searcher.Get();
//        }
//        catch (SystemException ex)
//        {
//            throw new SystemException($"Error getting WMI Information: {ex.Message}");
//        }
//    }

//    public List<WmiProperty> GetProperties(bool skipNull)
//    {
//        var properties = new List<WmiProperty>();
//        if (managementObject == null) return properties;

//        foreach (var property in managementObject.Properties)
//            if (!skipNull || property.Value != null)
//            {
//                var wmiProperty = new WmiProperty(property);
//                properties.Add(wmiProperty);
//            }

//        return properties;
//    }

//    public T? GetPropertyValue<T>(ManagementBaseObject? mbo, string propertyName)
//    {
//        // Return default value if the input object is null.
//        if (mbo == null) return default;

//        // Validate input.
//        if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException(null, nameof(propertyName));

//        // Find the property with the specified name.
//        var property = mbo.Properties.Cast<PropertyData>()
//            .FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

//        // convert System.String[] to System.String. the WMI query returns a string array for some properties (e.g. "192.168.1.1,1.1.1.1")
//        // the string should be converted to a single string with a comma separator (e.g. "192.168.1.1" ,"1.1.1.1")
//        if (property != null && property.Value is string[] stringArray)
//            return (T)(object)string.Join(", ", stringArray);

//        // Return default value if the property is null or if the property value is not of the expected type.
//        if (property?.Value == null || !typeof(T).IsAssignableFrom(property.Value.GetType())) return default;


//        // Handle cases where the property value is a string.
//        if (property.Value is string valueString)
//            // Return default value if the string is empty or if it cannot be converted to the expected type.
//            if (string.IsNullOrEmpty(valueString) || !IsStringConvertibleTo<T>(valueString))
//                return default;


//        // Convert the property value to the expected type and return it. if is System.String[] convert to System.String
//        return (T)Convert.ChangeType(property.Value, typeof(T));
//    }

//    public T? GetSystemPropertyValue<T>(string propertyName)
//    {
//        // Return default value if the management object is null.
//        if (managementObject == null) return default;

//        // Validate input.
//        if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException(null, nameof(propertyName));

//        // Find the property with the specified name.
//        var property = managementObject.SystemProperties.Cast<PropertyData>()
//            .FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

//        // Return default value if the property is null or if the property value is not of the expected type.
//        if (property?.Value == null || !typeof(T).IsAssignableFrom(property.Value.GetType())) return default;

//        // Handle cases where the property value is a string.
//        if (property.Value is string valueString)
//            // Return default value if the string is empty or if it cannot be converted to the expected type.
//            if (string.IsNullOrEmpty(valueString) || !IsStringConvertibleTo<T>(valueString))
//                return default;

//        // Handle cases where the property value is a array of string.
//        if (property.Value is string[] valueStringArray)
//            // Return default value if the array is empty or if it cannot be converted to the expected type.
//            if (valueStringArray.Length == 0 || !IsStringConvertibleTo<T>(valueStringArray[0]))
//                return default;

//        // Convert the property value to the expected type and return it.
//        return (T)Convert.ChangeType(property.Value, typeof(T));
//    }

//    /// <summary>
//    ///     Checks if a string can be converted to a specified type.
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="value"></param>
//    /// <returns></returns>
//    private static bool IsStringConvertibleTo<T>(string value)
//    {
//        try
//        {
//            var converter = TypeDescriptor.GetConverter(typeof(T));
//            converter.ConvertFromString(value);
//            return true;
//        }
//        catch (NotSupportedException)
//        {
//            return false;
//        }
//    }

//    public T? GetPropertyValue<T>(string propertyname)
//    {
//        return GetPropertyValue<T>((ManagementBaseObject)managementObject, propertyname);
//    }

//    public static List<T> Get<T>(string scope, string path) where T : WmiBase, new()
//    {
//        var objList = new List<T>();
//        try
//        {
//            var wmiObject = GetWMIObject(scope, $"Select * From {path}");
//            if (wmiObject == null) return objList;
//            foreach (ManagementObject managementObject in wmiObject)
//            {
//                var obj1 = new T
//                {
//                    managementObject = managementObject
//                };
//                var obj2 = obj1;
//                objList.Add(obj2);
//            }
//        }
//        catch (SystemException ex)
//        {
//            throw new SystemException($"Error getting WMI Information: {ex.Message}");
//        }

//        return objList;
//    }

//    public void Dump(bool skipNull)
//    {
//        try
//        {
//            foreach (var property in GetProperties(skipNull)) property.Dump(skipNull);
//        }
//        catch (SystemException ex)
//        {
//            throw new SystemException($"Error Dumping WMI Information: {ex.Message}");
//        }
//    }
//}

#nullable enable
using System.ComponentModel;
using System.Management;
using System.Runtime.Versioning;

namespace HPIndigoSysValTool.SystemInfo.Windows;

[SupportedOSPlatform("windows")]
public class WmiBase : IDisposable
{
    protected ManagementObject? managementObject;

    public virtual void Dispose()
    {
        managementObject?.Dispose();
    }

    protected static ManagementObjectCollection? GetWMIObject(string scope, string query)
    {
        using var searcher = new ManagementObjectSearcher(scope, query);
        return searcher.Get();
    }

    public List<WmiProperty> GetProperties(bool skipNull)
    {
        var properties = new List<WmiProperty>();
        if (managementObject == null) return properties;

        foreach (var property in managementObject.Properties)
            if (!skipNull || property.Value != null)
            {
                var wmiProperty = new WmiProperty(property);
                properties.Add(wmiProperty);
            }

        return properties;
    }

    public T? GetPropertyValue<T>(string propertyName, bool isSystemProperty = false)
    {
        if (managementObject == null) return default;

        if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException(null, nameof(propertyName));

        var properties = isSystemProperty ? managementObject.SystemProperties : managementObject.Properties;
        var property = properties.Cast<PropertyData>()
            .FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

        if (property?.Value is string[] stringArray)
            return (T)(object)string.Join(", ", stringArray);

        if (property?.Value == null || property.Value is not T value)
            return default;

        return value;
    }

    public static List<T> Get<T>(string scope, string path) where T : WmiBase, new()
    {
        var objList = new List<T>();
        var wmiObject = GetWMIObject(scope, $"Select * From {path}");
        if (wmiObject == null) return objList;

        foreach (ManagementObject managementObject in wmiObject)
        {
            var obj1 = new T
            {
                managementObject = managementObject
            };
            objList.Add(obj1);
        }

        return objList;
    }

    public void Dump(bool skipNull)
    {
        foreach (var property in GetProperties(skipNull)) property.Dump(skipNull);
    }
}
