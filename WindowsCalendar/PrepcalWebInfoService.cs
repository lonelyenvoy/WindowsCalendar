using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsCalendar
{
    /// <summary>
    /// 万年历网络信息服务，用于获取最新版本及界面显示信息等
    /// </summary>
    public static class PrepcalWebInfoService
    {
        private static bool isInitialized = false;
        public static bool IsInitialized
        {
            get { return isInitialized; }
        }

        // 初始化
        public static void Initializate()
        {
            if (!ApplicationStatus.isInternetConnected)
                throw new WebException();

            string webInfo = WebServices.GetWebClient(ApplicationStatus.prepcalWebInfoUrl);

            // webInfo的示例
            //string webInfo = "<prepcal><version>1.27</version><donateCount>1</donateCount><homepage>www.baidu.com</homepage>
            //<apiUrlJisu>http://api.jisuapi.com/huangli/date?appkey=[key]&year=[year]&month=[month]&day=[day]</apiUrlJisu>
            //<apiKeyJisu>9969a22083bd856e</apiKeyJisu>
            //<apiUrlJuhe>http://japi.juhe.cn/calendar/day?date=[year]-[month]-[day]&key=[key]</apiUrlJuhe>
            //<apiKeyJuhe>cea509aba88b9a19390b5dfcf9a57451</apiKeyJuhe></prepcal>";

            string[] patterns =
            {
                @"\<prepcal\>.*\<version\>(?<result>.+)\</version\>.*\</prepcal\>", // versionPattern
                @"\<prepcal\>.*\<donateCount\>(?<result>.+)\</donateCount\>.*\</prepcal\>", // donateCountPattern
                @"\<prepcal\>.*\<donateInfo\>(?<result>.+)\</donateInfo\>.*\</prepcal\>", // donateInfoPattern
                @"\<prepcal\>.*\<homepage\>(?<result>.+)\</homepage\>.*\</prepcal\>", // homepagePattern
                @"\<prepcal\>.*\<notice\>(?<result>.+)\</notice\>.*\</prepcal\>" // noticePattern
            };

            // 将patterns中的<>替换为html格式
            for (int i = 0; i < patterns.Length; i++)
            {
                patterns[i] = patterns[i].Replace(@"\<", "&lt;");
                patterns[i] = patterns[i].Replace(@"\>", "&gt;");
            }

            // 开始匹配
            Regex versionReg = new Regex(patterns[0], RegexOptions.ExplicitCapture);
            Regex donateCountReg = new Regex(patterns[1], RegexOptions.ExplicitCapture);
            Regex donateInfoReg = new Regex(patterns[2], RegexOptions.ExplicitCapture);
            Regex homepageReg = new Regex(patterns[3], RegexOptions.ExplicitCapture);
            Regex noticeReg = new Regex(patterns[4], RegexOptions.ExplicitCapture);
            string latestVersion = versionReg.Match(webInfo).Groups["result"].Value;
            string donateManTimeCount = donateCountReg.Match(webInfo).Groups["result"].Value;
            string donateInfo = donateInfoReg.Match(webInfo).Groups["result"].Value;
            string homepage = homepageReg.Match(webInfo).Groups["result"].Value;
            string notice = noticeReg.Match(webInfo).Groups["result"].Value;

            // 将结果存入ApplicationStatus类
            if (latestVersion != "")
                ApplicationStatus.latestVersion = latestVersion;
            if (donateManTimeCount != "")
                ApplicationStatus.donateManTimeCount = donateManTimeCount;
            if (donateInfo != "")
            {
                donateInfo = donateInfo.Replace("&nbsp;", " ");
                donateInfo = donateInfo.Replace("#enter", "\n");
                ApplicationStatus.donateInfo = donateInfo;
            }
            if (homepage != "")
                ApplicationStatus.homepage = homepage;
            if (notice != "")
            {
                notice = notice.Replace("&nbsp;", " ");
                notice = notice.Replace("#enter", "\n");
                ApplicationStatus.notice = notice;
            }

            isInitialized = true;
        }
    }
}
