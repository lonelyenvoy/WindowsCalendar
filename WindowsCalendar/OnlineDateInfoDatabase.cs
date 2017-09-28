using System;
using System.Text.RegularExpressions;

namespace WindowsCalendar
{
    /// <summary>
    /// 在线黄历信息数据库，存储当月所有天数的黄历
    /// </summary>
    public static class OnlineDateInfoDatabase
    {
        private const int MAXMONTHSTORAGE = 1200; // 120

        public readonly static DateTime MINDATE = new DateTime(1901, 1, 1);
        public readonly static DateTime MAXDATE = new DateTime(2049, 12, 31);

        private static bool isInitialized = false; // 将来版本可能会用到的初始化flag
        public static event MyEventHandlerArgs NewMonthQueried; // 新月份已查询事件，在DynamicApiQueryQueue和MainForm中被处理

        private static void OnNewMonthQueried(DateMonth dateMonth)
        {
            NewMonthQueried?.Invoke(dateMonth);
        }

        private static string[,] dateInfosInMonth = new string[MAXMONTHSTORAGE, 32]; // 黄历数据字符串，第一维为月份索引，第二维为日期索引，[1]~[31]有效
        private static DateMonth[] months = new DateMonth[MAXMONTHSTORAGE]; // 月份信息存储器，记录dateInfosInMonth的第一维索引的指向
        private static int storedMonthCount = 0; // 已存储的月份计数

        private static bool isFirstLoad = true; // 是否为第一次apiQuery
        private static int initProgess = 0; // 初始化时加载的进度（0~100），只读
        private static int initCurrentDay = 0; // 初始化时正在读取的天数，只读
        public static int InitProgess
        {
            get { return initProgess; }
        }
        public static int InitCurrentDay
        {
            get { return initCurrentDay; }
        }

        // 外部初始化调用入口
        public static void Initialize()
        {
            DynamicApiQueryQueue.NewMonthQueryRequested += new MyEventHandlerArgs(NewMonthQueryRequestedHandler);
            isInitialized = true;
        }


        // 内部初始化，调用WebServices类中的ApiQuery从Webapi读取数据
        private static void InitDateInfo(int year, int month)
        {
            if (year < 1 || year > 9999 || month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(); // 参数错误

            initProgess = 0;
            //DateTime sampleDate = new DateTime(year, month, 1);
            months[storedMonthCount] = new DateMonth(year, month); // 存储月份索引

            int maxDayInMonth = DateTime.DaysInMonth(year, month); // 当前月的天数
            for (int i = 0; i < maxDayInMonth; i++)
            {
                // 仅在第一次加载时（MainForm还未Show）向外界通报进度信息
                if (isFirstLoad)
                {
                    initCurrentDay = i + 1; // 记录正在初始化的天数
                    initProgess = initCurrentDay * 100 / maxDayInMonth; // 记录初始化进度
                }
                DateTime date = new DateTime(year, month, i + 1);
                dateInfosInMonth[storedMonthCount, i + 1] = WebServices.ApiQueryOnJuhe(date); // 查询黄历信息
                Console.WriteLine(year.ToString() + "/" + month.ToString() + "/" + (i + 1).ToString());
            }
            isFirstLoad = false;
            storedMonthCount++;
        }

        private static void NewMonthQueryRequestedHandler(object o)
        {
            DateMonth newMonth = o as DateMonth;
            if (newMonth == null)
                throw new ArgumentException();

            if (searchMonth(newMonth.Year, newMonth.Month) != -1) // 异常：指定月份的黄历信息已存在于数据库中
            {
                // ignore
            }
            else
            {
                InitDateInfo(newMonth.Year, newMonth.Month);
            }
            OnNewMonthQueried(newMonth);
        }

        // 寻找指定月份（DateTime参数中的月份）在数据库中的索引，失败返回-1
        public static int searchMonth(DateTime date)
        {
            int dataStorageIndex = -1;

            for (int i = 0; i < storedMonthCount; i++)
            {
                if (months[i].EqualsTo(date))
                {
                    dataStorageIndex = i;
                    break;
                }
            }

            return dataStorageIndex;
        }

        // 寻找指定月份（Year, Month参数决定的月份）在数据库中的索引，失败返回-1
        public static int searchMonth(int year, int month)
        {
            if (year < 1 || year > 9999 || month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(); // 参数错误

            return searchMonth(new DateTime(year, month, 1));
        }

        // 刷新指定月份的数据库，isForce为真时强制刷新
        public static void Refresh(int year, int month, bool isForce = false)
        {
            if (searchMonth(year, month) != -1 && isForce == false) // 指定月份的黄历信息已存在于数据库中
            {
                return;
            }
            InitDateInfo(year, month);
        }

        // 得到某一月份在数据库中的索引，如果没有此月份，则初始化刷新数据库
        private static int getDataStorageIndex(DateTime date)
        {

            int dataStorageIndex = searchMonth(date);
            if (dataStorageIndex == -1) // 未找到数据
            {
                InitDateInfo(date.Year, date.Month);
                dataStorageIndex = storedMonthCount - 1;
            }
            return dataStorageIndex;
        }

        // 取得某一特定日期的完整农历日期（如：七月十七）
        public static string GetLunarCalendarMonthDay(DateTime date)
        {
            int dataStorageIndex = getDataStorageIndex(date);

            // 正则表达式
            // example
            //  , "lunarYear" : "甲午年" , "lunar" : "十一月十一" , "year-month" : "2015-1" , "date" : "2015-1-1"}}}

            string pattern = @"\u0022lunar\u0022:\u0022(?<result>\w+)\u0022"; // \u0022表示双引号
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(dateInfosInMonth[dataStorageIndex, date.Day]).Groups["result"].Value;
        }

        // 取得某一特定日期的农历日期（如：十七）
        public static string GetLunarCalendarDay(DateTime date)
        {
            string fullResult = GetLunarCalendarMonthDay(date);
            string pattern = @"\w+月(?<result>\w+)";
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(fullResult).Groups["result"].Value;
        }

        // 取得某一特定日期的农历月份（如：七月）
        public static string GetLunarCalendarMonth(DateTime date)
        {
            string fullResult = GetLunarCalendarMonthDay(date);
            string pattern = @"(?<result>\w+月)\w";
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(fullResult).Groups["result"].Value;
        }

        // 取得某一特定日期的农历年份（如：甲午年）
        public static string GetLunarYearCalendar(DateTime date)
        {
            int dataStorageIndex = getDataStorageIndex(date);

            string pattern = @"\u0022lunarYear\u0022:\u0022(?<result>\w+)\u0022";
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(dateInfosInMonth[dataStorageIndex, date.Day]).Groups["result"].Value;
        }

        // 取得某一特定日期当年的生肖（如：马）
        public static string GetAnimalsYearCalendar(DateTime date)
        {
            int dataStorageIndex = getDataStorageIndex(date);

            string pattern = @"\u0022animalsYear\u0022:\u0022(?<result>\w+)\u0022";
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(dateInfosInMonth[dataStorageIndex, date.Day]).Groups["result"].Value;
        }

        // 取得某一特定日期的节假日信息（如：元旦，失败返回空字符串）
        public static string GetHoliday(DateTime date)
        {
            int dataStorageIndex = getDataStorageIndex(date);

            string pattern = @"\u0022holiday\u0022:\u0022(?<result>\w+)\u0022";
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(dateInfosInMonth[dataStorageIndex, date.Day]).Groups["result"].Value;
        }

    }
}
