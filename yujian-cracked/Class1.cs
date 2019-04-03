using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Management;
using System.Security.Cryptography;

internal sealed class Module1
{
    // Methods
    public static string AesEncrypt(byte[] SourceArray, byte[] KeyArray)
    {
        RijndaelManaged managed = new RijndaelManaged();
        RijndaelManaged managed2 = managed;
        managed2.Key = KeyArray;
        managed2.Mode = CipherMode.ECB;
        managed2.Padding = PaddingMode.Zeros;
        managed2 = null;
        return BitConverter.ToString(managed.CreateEncryptor().TransformFinalBlock(SourceArray, 0, SourceArray.Length)).Replace("-", "");
    }

    public static string GETCPUID()
    {
        string str2 = "";
        ManagementClass class2 = new ManagementClass("Win32_Processor");
        foreach (ManagementBaseObject obj3 in class2.GetInstances())
        {
            ManagementObject obj2 = (ManagementObject)obj3;
            str2 = obj2.Properties["ProcessorId"].Value.ToString();
        }
        return str2;
    }

    public static string GetDiskSerialNumber()
    {
        string str2 = "";
        ManagementClass class2 = new ManagementClass("Win32_DiskDrive");
        using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = class2.GetInstances().GetEnumerator())
        {
            if (enumerator.MoveNext())
            {
                str2 = Conversions.ToString(((ManagementObject)enumerator.Current).Properties["Model"].Value);
            }
        }
        return str2;
    }

    public static string GetMacAddress()
    {
        string str2 = "";
        ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");
        foreach (ManagementBaseObject obj3 in class2.GetInstances())
        {
            ManagementObject obj2 = (ManagementObject)obj3;
            if (Conversions.ToBoolean(obj2["IPEnabled"]))
            {
                str2 = obj2["MacAddress"].ToString();
                break;
            }
        }
        return str2;
    }
}
