using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace WindowsCalendar
{
    // 应用程序状态类
    public static class ApplicationStatus
    {
        // consts
        public const string version = "1.47";
        // previous webInfo in CSDN below (<=) 1.29
        //public const string prepcalWebInfoUrl = "http://blog.csdn.net/lonelyenvoy/article/details/52432835";
        //public const string updateDownloadUrl = "http://blog.csdn.net/lonelyenvoy";
        // current webInfo in cnblogs above (>=) 1.32
        public const string prepcalWebInfoUrl = "http://www.cnblogs.com/LonelyEnvoy/p/5850657.html";
        public const string updateDownloadUrl = "http://www.cnblogs.com/LonelyEnvoy";

        public const string metaFileName = "meta";
        public const string agreementSignedFlagFileName = "agreement";
        public const string firstRunNoInternetFlagFileName = "firstRunNoInternet";
        public readonly static string applicationDataFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            OfflineDateInfoStructure.APPLICATIONDATADIRECTORY);

        // status
        public static bool isInternetConnected = true;
        public static string latestVersion = "";
        public static string donateManTimeCount = "";
        public static string donateInfo = "";
        public static string homepage = "http://www.cnblogs.com/LonelyEnvoy";
        public static string notice = "";

        public static bool isAgreementSigned = false;
        public static bool isFirstRun = false;
        public static bool isVersionUpdated = false;
        public static bool isFirstRunNoInternet = false;

        public static string previousVersion = "";
    }
}
