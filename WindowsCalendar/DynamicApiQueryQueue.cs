using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsCalendar
{
    /// <summary>
    /// 动态队列查询
    /// </summary>
    public static class DynamicApiQueryQueue
    {
        //private static readonly string RESULTSAVINGPATH = 
        //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WindowsCalendar");
        //private const string RESULTFILENAME = "OnlineCalendarInfo";
        private const int MAXMONTHSTORAGE = 1200; // 600

        private static bool isInitialized = false; // 将来版本可能会用到的初始化flag
        private static bool isRunning = false;

        public static bool IsRunning
        {
            get { return isRunning; }
        }

        private static Queue<DateMonth> awaitingMonthsQueue = new Queue<DateMonth>(); // 待查询的月份队列
        private static List<DateMonth> queriedMonthList = new List<DateMonth>(); // 已查询的月份表
        private static DateMonth queryingMonth = null; // 正在查询的月份

        // 要求查询新月份事件，在OnlineDateInfoDatabase和DynamicApiQueryQueue中被处理
        public static event MyEventHandlerArgs NewMonthQueryRequested;

        // 原始思路：
        // 初始化时
        // 先在queriedMonthList中加入currentMonth
        // 调用AssignPriority方法进行优先级分配，创建摆动数列（跳过queriedMonthList中的月份），大小为±半年
        // 在DateMonth类中用Compare()方法求：指定月份dateMonth的优先级 = f(dateMonth与currentMonth距离)
        // 优先级分配完毕后，将元素加入awaitingMonthsQueue并按给定的优先级排序
        // 
        // 随后，启动工作线程，在后台用DynamicApiQuery方法（注意需跳过该月份中可能已经查询的部分日期）
        // 将awaitingMonthsQueue中的月份动态加载到OnlineDateInfoDatabase
        //
        // 注：1. 加载每年的数据耗时约50-60秒
        //     2. 可加载的最大期限：50年（±25年）

        // NewMonthQueried事件引发时，删除awaitingMonthsQueue的首元素，/*根据优先级加入下一个元素（如何限制元素数量？）*/
        // 将NewMonthQueried的参数（刚查询的新月份）加入queriedMonthList

        // CurrentMonthChanged事件引发时，调用AssignPriority方法重新分配优先级，创建摆动数列
        // 跳过已queried的月份
        // 如果JumpedToNonQueriedMonth，立即中断当前工作，保存部分数据，改为query当前月份


        // 事件处理程序
        private static void OnNewMonthQueryRequested(DateMonth dateMonth)
        {

            NewMonthQueryRequested?.Invoke(dateMonth);
        }

        private static void NewMonthQueryRequestedHandler(object o)
        {

            DateMonth newMonth = o as DateMonth;
            if (newMonth == null)
                throw new ArgumentException();

            // 将刚查询的月份加入queriedMonthList
            queriedMonthList.Add(newMonth);
        }

        private static void NewMonthQueriedHandler(object o)
        {

            DateMonth newMonth = o as DateMonth;
            if (newMonth == null)
                throw new ArgumentException();

            // 重新启动DynamicApiQuery线程
            if (awaitingMonthsQueue.Count != 0)
                DynamicApiQuery();
            else
            {
                Console.WriteLine("DynamicApiQueryFinished!");
                isRunning = false;
            }
        }

        private static void CurrentShowingMonthChangedHandler(object o)
        {
            DateMonth newMonth = o as DateMonth;
            if (newMonth == null)
                throw new ArgumentException();

            AssignPriority(newMonth); // 按当前月份为中心月份重新分配优先级
        }

        // 初始化
        public static void Initialize(DateMonth currentShowingMonth)
        {
            queriedMonthList.Add(currentShowingMonth); // 添加本月到已查询的月份列表

            // 事件布线
            DynamicApiQueryQueue.NewMonthQueryRequested += new MyEventHandlerArgs(NewMonthQueryRequestedHandler);
            OnlineDateInfoDatabase.NewMonthQueried += new MyEventHandlerArgs(NewMonthQueriedHandler);
            MainForm.CurrentShowingMonthChanged += new MyEventHandlerArgs(CurrentShowingMonthChangedHandler);

            AssignPriority(currentShowingMonth); // 为边界范围内的所有月份分配优先级
            DynamicApiQuery();// 启动工作线程

            isInitialized = true;
            isRunning = true;
        }


        // 分配ApiQuery任务中月份的优先级
        private static void AssignPriority(DateMonth centralMonth)
        {
            int assignedMonthCount = 0;

            // 清空awaitingMonthsQueue
            awaitingMonthsQueue.Clear();

            // 分配顺序：摆动数列，如centralMonth为2016.8，队列为：(2016.8), 2016.9, 2016.7, 2016.10, 2016.6 ...
            for (int i = 0, loopCount = 0; assignedMonthCount < MAXMONTHSTORAGE; loopCount++)
            {
                i = -i;
                if ((loopCount + 1) % 2 == 0) // 每隔一次待分配的月份加1
                    i++;

                DateMonth month = centralMonth.AddMonth(i);
                // 跳过已queried的月份
                if (!queriedMonthList.Exists(var => var.Year == month.Year && var.Month == month.Month))
                {
                    awaitingMonthsQueue.Enqueue(month); // 加入队列
                    assignedMonthCount++;
                }
            }
        }

        // 动态查询Api，核心方法
        private static void DynamicApiQuery()
        {
            Task queryTask = Task.Factory.StartNew(delegate
            {
                Thread.Sleep(1000); // 每分配完一个月份后阻塞线程，防止占用大量带宽

                if (awaitingMonthsQueue.Count > 0)
                {
                    queryingMonth = awaitingMonthsQueue.Dequeue();
                    OnNewMonthQueryRequested(queryingMonth); // 请求查询api
                }
                else
                    throw new InvalidOperationException(); // 发生未知错误，队列中无元素
            });
        }

    }
}
