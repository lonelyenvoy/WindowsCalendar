using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsCalendar
{
    /// <summary>
    /// 离线黄历信息结构，获取所有日期的黄历
    /// </summary>
    public static class OfflineDateInfoStructure
    {
        // 离线结构存储的起始日期及结束日期
        public static readonly DateTime STARTDATE = new DateTime(2014, 1, 1);
        public static readonly DateTime ENDDATE = new DateTime(2018, 12, 31);

        private static readonly int SPANDAYS = (ENDDATE - STARTDATE).Days + 1;
        private static readonly string INFO = "\n---------- api抓取程序 ----------\n\n总任务天数：" + SPANDAYS + "天。确认开始？\n按任意键启动工作线程\n";

        private static readonly string[] ITEMNAMES =
        {
            "日期索引",
            "年",         // year [1]
            "月",         // month [2]
            "日",         // day [3]
            "阳历",       // yangli [4]
            "农历",       // nongli [5]
            "星座",       // xingzuo [6]
            "胎神",       // taishen [7]
            "五行",       // wuxing [8]
            "冲",         // chong [9]
            "煞",         // sha [10]
            "生肖",       // shengxiao [11]
            "吉日",       // jiri [12]
            "值日天神",   // zhiri [13]
            "凶神",       // xiongshen [14]
            "吉神宜趋",   // jishenyiqu [15]
            "财神",       // caishen [16]
            "喜神",       // xishen [17]
            "福神",       // fushen [18]
            "岁次",       // suici [19]
            "宜",         // yi [20]
            "忌",         // ji [21]
            "英文星期",   // eweek [22]
            "英文月",     // emonth [23]
            "星期"        // week [24]
        };

        private const int MAXYEARSTORAGE = 56;
        private const int LUNARINFOLENGTH = 25; // 黄历数据的长度

        private static int currentLoadingDayCount = 0; // 当前正在读取的天数，用于外部线程访问
        public static int CurrentLoadingDayCount { get { return currentLoadingDayCount; } }

        public const string APPLICATIONDATADIRECTORY = @"WindowsCalendar";
        private const string DATAFILENAME = @"CalendarInfo.data";

        private static bool isDataLoaded = false; // 记录是否已加载数据
        private static string[,] storedDateInfos = new string[SPANDAYS, LUNARINFOLENGTH]; // 用于加载已存储的数据

        // 从硬盘加载数据到内存，失败返回false
        public static bool LoadDateInfos()
        {
            // 先将资源表中的dateInfo.data导出到目录下
            string compressedFilePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATIONDATADIRECTORY, DATAFILENAME);
            string applicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATIONDATADIRECTORY);

            // 判断ApplicationData目录是否存在
            if (Directory.Exists(applicationDataPath))
            {

            }
            else
            {
                // 创建ApplicationData文件夹
                Directory.CreateDirectory(applicationDataPath);
            }

            // 判断待导出的文件是否存在，如不存在则导出
            if (!System.IO.File.Exists(Path.Combine(applicationDataPath, DATAFILENAME)))
            {
                // 写出资源
                byte[] res = new byte[WindowsCalendar.Properties.Resources.CalendarInfo.Length]; // 要确定数组大小，否则报错
                WindowsCalendar.Properties.Resources.CalendarInfo.CopyTo(res, 0); // 将资源导入byte数组中

                // 将res写入文件
                FileStream fsExport = new FileStream(compressedFilePath, FileMode.Create, FileAccess.Write);
                fsExport.Write(res, 0, res.Length);
                fsExport.Close();
            }

            // 解压
            FileInfo compressedFile = new FileInfo(compressedFilePath);
            if (!compressedFile.Exists)
                return false;
            ZipUtil.Decompress(compressedFile, false);
            Regex reg = new Regex(@"(?<path>.+)\..+", RegexOptions.ExplicitCapture); // 去除扩展名
            FileInfo decompressedFile = new FileInfo(reg.Match(compressedFilePath).Groups["path"].Value);

            FileStream fs = new FileStream(decompressedFile.FullName, FileMode.Open);
            BinaryReader bw = new BinaryReader(fs);

            for (int i = 0; i < SPANDAYS; i++)
            {
                for (int j = 0; j < LUNARINFOLENGTH; j++)
                {
                    storedDateInfos[i, j] = bw.ReadString();
                }
            }
            bw.Close();

            // 删除从资源表中导出的已解压文件，节省磁盘空间
            //File.Delete(compressedFile.FullName);
            System.IO.File.Delete(decompressedFile.FullName);

            // 检验数据是否正常
            if (storedDateInfos[SPANDAYS - 1, 0] == (SPANDAYS - 1).ToString())
            {
                isDataLoaded = true;
                return true;
            }
            return false;
        }

        // 获取某一天的所有黄历信息数组
        private static string[] GetDateInfo(DateTime date)
        {
            if (date < STARTDATE || date > ENDDATE)
                throw new ArgumentOutOfRangeException();
            if (!isDataLoaded)
                throw new InvalidOperationException();

            int spanDays = (date - STARTDATE).Days;
            string[] tempInfo = new string[LUNARINFOLENGTH];
            for (int i = 0; i < LUNARINFOLENGTH; i++)
            {
                tempInfo[i] = storedDateInfos[spanDays, i];
            }
            return tempInfo;
        }

        // 获取某一天的完整农历信息（如：农历二〇一六年六月廿九）
        public static string GetLunarFull(DateTime date)
        {
            return GetDateInfo(date)[5];
        }

        // 获取某一天的农历月份（如：六月）
        public static string GetLunarMonth(DateTime date)
        {
            string fullResult = GetLunarFull(date);

            string pattern = @".+年(?<result>\w+月)\w+";
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(fullResult).Groups["result"].Value;
        }

        // 获取某一天的农历日（如：廿九）
        public static string GetLunarDay(DateTime date)
        {
            string fullResult = GetLunarFull(date);

            string pattern = @".+月(?<result>\w+)";
            Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
            return reg.Match(fullResult).Groups["result"].Value;
        }

        // 获取某一天的星座
        public static string GetStar(DateTime date)
        {
            return GetDateInfo(date)[6];
        }

        // 获取某一天的胎神
        public static string GetTaishen(DateTime date)
        {
            return GetDateInfo(date)[7];
        }

        // 获取某一天的五行
        public static string GetWuxing(DateTime date)
        {
            return GetDateInfo(date)[8];
        }

        // 获取某一天的冲
        public static string GetChong(DateTime date)
        {
            return GetDateInfo(date)[9];
        }

        // 获取某一天的煞
        public static string GetSha(DateTime date)
        {
            return GetDateInfo(date)[10];
        }

        // 获取某一天的生肖
        public static string GetAnimal(DateTime date)
        {
            return GetDateInfo(date)[11];
        }

        // 获取某一天的吉日
        public static string GetLuckyDay(DateTime date)
        {
            return GetDateInfo(date)[12];
        }

        // 获取某一天的值日天神
        public static string GetZhiRi(DateTime date)
        {
            return GetDateInfo(date)[13];
        }

        // 获取某一天的凶神
        public static string GetXiongShen(DateTime date)
        {
            return GetDateInfo(date)[14];
        }

        // 获取某一天的吉神宜趋
        public static string GetJiShen(DateTime date)
        {
            return GetDateInfo(date)[15];
        }

        // 获取某一天的财神
        public static string GetCaiShen(DateTime date)
        {
            return GetDateInfo(date)[16];
        }

        // 获取某一天的喜神
        public static string GetXiShen(DateTime date)
        {
            return GetDateInfo(date)[17];
        }

        // 获取某一天的福神
        public static string GetFuShen(DateTime date)
        {
            return GetDateInfo(date)[18];
        }

        // 获取某一天的岁次
        public static string GetSuiCi(DateTime date)
        {
            return GetDateInfo(date)[19];
        }

        // 获取某一天的宜
        public static string GetSuit(DateTime date)
        {
            return GetDateInfo(date)[20];
        }

        // 获取某一天的忌
        public static string GetAvoid(DateTime date)
        {
            return GetDateInfo(date)[21];
        }

        // 获取某一天的英文星期
        public static string GetEnglishWeek(DateTime date)
        {
            return GetDateInfo(date)[22];
        }

        // 获取某一天的英文月
        public static string GetEnglishMonth(DateTime date)
        {
            return GetDateInfo(date)[23];
        }

        // 调用WebServices类中的ApiQuery从Webapi读取数据并保存到string[,]中
        private static string[,] GetDateInfos(DateTime startDate, DateTime endDate)
        {
            TimeSpan span = endDate - startDate;
            int maxDaysStorage = MAXYEARSTORAGE * 12 * 31;
            if (span.Days > maxDaysStorage)
                throw new ArgumentOutOfRangeException();

            // 黄历数据字符串，每一行的首元素为日期索引，其余为黄历数据
            string[,] dateInfos = new string[span.Days, LUNARINFOLENGTH];
            string tempLunarInfo = "";
            DateTime currentDate = startDate;

            // 正则表达式，\u0022表示双引号
            string[] patterns = {
                        @"\u0022year\u0022:\u0022(?<1>\w+)\u0022", // year
                        @"\u0022month\u0022:\u0022(?<2>\w+)\u0022", // month
                        @"\u0022day\u0022:\u0022(?<3>\w+)\u0022", // day
                        @"\u0022yangli\u0022:\u0022(?<4>\w+)\u0022", // yangli
                        @"\u0022nongli\u0022:\u0022(?<5>.+?)\u0022", // nongli
                        @"\u0022star\u0022:\u0022(?<6>\w+)\u0022", // star
                        @"\u0022taishen\u0022:\u0022(?<7>\w+)\u0022", // taishen
                        @"\u0022wuxing\u0022:\u0022(?<8>\w+)\u0022", // wuxing
                        @"\u0022chong\u0022:\u0022(?<9>.+?)\u0022", // chong
                        @"\u0022sha\u0022:\u0022(?<10>\w+)\u0022", // sha
                        @"\u0022shengxiao\u0022:\u0022\s*(?<11>.+?)\u0022", // shengxiao
                        @"\u0022jiri\u0022:\u0022(?<12>.+?)\u0022", // jiri
                        @"\u0022zhiri\u0022:\u0022(?<13>.+?)\u0022", // zhiri
                        @"\u0022xiongshen\u0022:\u0022(?<14>.+?)\u0022", // xiongshen
                        @"\u0022jishenyiqu\u0022:\u0022(?<15>.+?)\u0022", // jishenyiqu
                        @"\u0022caishen\u0022:\u0022(?<16>\w+)\u0022", // caishen
                        @"\u0022xishen\u0022:\u0022(?<17>\w+)\u0022", // xishen
                        @"\u0022fushen\u0022:\u0022(?<18>\w+)\u0022", // fushen
                        @"\u0022suici\u0022:\[(?<19>(\u0022\w+\u0022,){0,}\u0022\w+\u0022)\]", // suici (raw)
                        @"\u0022yi\u0022:\[(?<20>(\u0022\w+\u0022,){0,}\u0022\w+\u0022)\]", // yi (raw)
                        @"\u0022ji\u0022:\[(?<21>(\u0022\w+\u0022,){0,}\u0022\w+\u0022)\]", // ji (raw)
                        @"\u0022eweek\u0022:\u0022(?<22>\w+)\u0022", // eweek
                        @"\u0022emonth\u0022:\u0022(?<23>\w+)\u0022", // emonth
                        @"\u0022week\u0022:\u0022(?<24>\w+)\u0022", // week
                    };
            Regex[] reg = new Regex[24];
            for (int i = 0; i < reg.Length; i++)
            {
                // 为提高运行效率，在需要加载离线数据库时以编译方式初始化正则表达式
                reg[i] = new Regex(patterns[i], RegexOptions.ExplicitCapture /*| RegexOptions.Compiled*/);
            }

            for (int i = 0; i < span.Days; i++)
            {
                currentLoadingDayCount = (currentDate - startDate).Days + 1;

                // 将每一行的首元素设置为索引，即当前日期相比初始日期经过的天数
                // 范围为0 ~ (MAXYEARSTORAGE * 12 * 31 - 1)
                dateInfos[i, 0] = (currentDate - startDate).Days.ToString();
                tempLunarInfo = WebServices.ApiQueryOnJisu(currentDate); // 查询api数据并临时保存到字符串内
                for (int j = 1; j < LUNARINFOLENGTH; j++) // 注意：从1开始，因为首元素是索引
                {
                    dateInfos[i, j] = reg[j - 1].Match(tempLunarInfo).Groups[j.ToString()].Value; // 进行既定规则的正则匹配
                    if (j == 19 || j == 20 | j == 21) // 当遍历到匹配结果的suici, yi, ji这几个多元结果时，删除其中的分隔符
                    {
                        dateInfos[i, j] = dateInfos[i, j].Replace("\",", " "); // 将匹配出的raw结果中的 ", 替换为空格
                        dateInfos[i, j] = dateInfos[i, j].Replace("\"", ""); // 删除所有的引号
                    }
                }
                currentDate = currentDate.AddDays(1); // 下一天
            }
            return dateInfos;
        }

        private static void ExecuteDateInfoOfflineProcess()
        {
            Console.WriteLine(INFO);
            Console.ReadKey(true);

            Task showTask = Task.Factory.StartNew(ShowProgressThread);
            string[,] dateInfos = OfflineDateInfoStructure.GetDateInfos(STARTDATE, ENDDATE);

            Thread.Sleep(1000);
            Console.WriteLine("数据读入完毕！\n");

            string filePath = @"F:\CalendarInfo";
            FileStream fs = new FileStream(filePath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            foreach (var v in dateInfos)
            {
                bw.Write(v);
            }
            bw.Close();

            Console.WriteLine("数据存储完毕！\n");

            ZipUtil.Compress(new FileInfo(filePath), ".data", false);
            Console.WriteLine("数据压缩完毕！\n");

            Console.ReadKey(true);
        }

        private static void ShowProgressThread()
        {
            while (true)
            {
                TimeSpan span = ENDDATE - STARTDATE;
                Console.Clear();
                Console.WriteLine(INFO);
                Console.Write("\n正在读取数据并存入数据库: 第{0}天 / 共{1}天", OfflineDateInfoStructure.CurrentLoadingDayCount, span.Days);

                if (OfflineDateInfoStructure.CurrentLoadingDayCount == span.Days)
                {
                    Console.WriteLine();
                    return; // loading complete, thread end
                }

                Thread.Sleep(100);
            }
        }
    }
}