using System;
using System.Management;

namespace WindowsCalendar
{
    public class Hardware
    {
        // 以下代码出处（有删改）：http://blog.csdn.net/songkexin/article/details/4916602
        // 版权归原作者所有

        /// <summary>
        /// 取第一块硬盘编号，失败返回"Unknown"
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                string strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "Unknown";
            }
        }//end    
    }
}
