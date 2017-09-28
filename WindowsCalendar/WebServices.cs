using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WindowsCalendar
{
    public static class WebServices
    {
        // 极速数据：已下载并存入离线数据库
        // 聚合数据：在线api

        private const string apiKeyJisu = "9969a22083bd856e";
        private const string apiUrlJisu = "http://api.jisuapi.com/huangli/date"; //http://api.jisuapi.com/huangli/date?appkey=yourappkey&year=2015&month=10&day=27
        private const string apiKeyJuhe = "cea509aba88b9a19390b5dfcf9a57451";
        private const string apiUrlJuhe = "http://v.juhe.cn/calendar/day?date="; // http://v.juhe.cn/calendar/day?date=2015-1-1&key=cea509aba88b9a19390b5dfcf9a57451

        public static string ApiQueryOnJisu(DateTime date)
        {
            //string dateStr = date.ToString();
            //string dateStandardStr = dateStr.Substring(0, dateStr.IndexOf(" ")).Replace("/", "-"); // 将"2016/1/1 0:00:00"格式转换为"2016-1-1"
            //string dateStandardStr = date.ToString("yyyy-M-d");

            string apiCompleteUrl = apiUrlJisu + "?appkey=" + apiKeyJisu
                + "&year=" + date.Year + "&month=" + date.Month + "&day=" + date.Day;
            return GetWebClient(apiCompleteUrl);
        }

        public static string ApiQueryOnJuhe(DateTime date)
        {
            string dateStandardStr = date.ToString("yyyy-M-d");
            string apiCompleteUrl = apiUrlJuhe + dateStandardStr + "&key=" + apiKeyJuhe;
            return GetWebClient(apiCompleteUrl);
        }

        public static string GetWebClient(string url)
        {
            string strHTML = "";
            try
            {
                WebClient myWebClient = new WebClient();
                Stream myStream = myWebClient.OpenRead(url);
                StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = sr.ReadToEnd();
                myStream.Close();
                return strHTML;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
