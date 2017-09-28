using System;

namespace WindowsCalendar
{
    /// <summary>
    /// 月份类，用于与DateTime类型协作
    /// </summary>
    public class DateMonth
    {
        //private int year;
        //private int month;
        public int Year { get; }
        public int Month { get; }

        public DateMonth(int year, int month)
        {
            if (year < 0 || year > 9999 || month < 1 || month > 12)
                throw new ArgumentOutOfRangeException();

            this.Year = year;
            this.Month = month;
        }

        public DateMonth(DateTime date)
        {
            this.Year = date.Year;
            this.Month = date.Month;
        }

        /// <summary>
        /// 判断与dateTime类型的年份及月份是否相等
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool EqualsTo(DateTime dateTime)
        {
            return dateTime.Year == this.Year && dateTime.Month == this.Month ? true : false;
        }

        /// <summary>
        /// 比较两个DateMonth类型相距的月份数，返回正数说明this更大，否则anotherMonth更大
        /// </summary>
        /// <param name="anotherMonth">待比较的月份</param>
        /// <returns></returns>
        public int Compare(DateMonth anotherMonth)
        {
            if (this.Year == anotherMonth.Year)
                return this.Month - anotherMonth.Month;
            else if (this.Year - anotherMonth.Year == 1)
                return this.Month + (12 - anotherMonth.Month);
            else if (this.Year - anotherMonth.Year == -1)
                return -((12 - this.Month) + anotherMonth.Month);
            else if (this.Year - anotherMonth.Year > 1)
                return (this.Year - anotherMonth.Year - 1) * 12 + this.Month + (12 - anotherMonth.Month);
            else
                return -(this.Year - anotherMonth.Year - 1) * 12 + -((12 - this.Month) + anotherMonth.Month);
        }

        /// <summary>
        /// 比较该月份与另一月份相距的月份数，返回正数说明this更大，否则anotherMonth更大
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public int Compare(int year, int month)
        {
            DateMonth dateMonth = new DateMonth(year, month);
            return Compare(dateMonth);
        }

        /// <summary>
        /// 返回在指定月份的基础上增加n个月份后得到的月份
        /// </summary>
        /// <param name="month">要增加的月份数</param>
        /// <returns></returns>
        public DateMonth AddMonth(int month)
        {
            if (this.Month + month >= 1 && this.Month + month <= 12)
                return new DateMonth(this.Year, this.Month + month);
            else
            {
                int newYear = this.Year;
                int newMonth = this.Month + month;

                if (month > 0)
                {
                    while (newMonth > 12)
                    {
                        newMonth -= 12;
                        newYear++;
                    }
                }
                else
                {
                    while (newMonth < 1)
                    {
                        newMonth += 12;
                        newYear--;
                    }
                }
                return new DateMonth(newYear, newMonth);
            }
        }

        /// <summary>
        /// 返回指定月份的索引（与公元0年0月相距的月份数）
        /// </summary>
        /// <returns></returns>
        public int ToIndex()
        {
            return Compare(0, 0);
        }

        /// <summary>
        /// 返回索引指定的DateMonth类型
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static DateMonth ToDateMonth(int index)
        {
            return new DateMonth(0, 0).AddMonth(index);
        }
    }
}