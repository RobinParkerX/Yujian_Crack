using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using System.Management;
using System.Security.Cryptography;
using System.Web.Security;

namespace yujian_cracked
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            Console.WriteLine("Cracked by L4apQ");
            Console.WriteLine(keyGen());
            Console.Read();
        }
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

        [Obsolete]
        private static string keyGen()
        {
            byte[] keyArray = new byte[] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15};
            string s = FormsAuthentication.HashPasswordForStoringInConfigFile(Module1.GETCPUID()+ Module1.GetDiskSerialNumber()+ Module1.GetMacAddress(), "SHA1");

            return Module1.AesEncrypt(Encoding.Default.GetBytes(s), keyArray);
        }
    }
}

