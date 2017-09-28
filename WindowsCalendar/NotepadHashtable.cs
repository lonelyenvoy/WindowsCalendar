using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsCalendar
{
    /// <summary>
    /// 记事本的哈希表存储结构
    /// </summary>
    public static class NotepadHashtable
    {
        // 用于存储数据的哈希表
        private static Hashtable dataHashtable = new Hashtable();
        private static int storageCount = 0;

        public static Hashtable DataHashTable
        {
            get { return dataHashtable; }
        }

        // TextChanged事件集，在NotepadStorage中被处理
        public static event MyEventHandler DataHashtableUpdated;

        // TextChanged事件处理程序
        private static void OnDataHashtableUpdated()
        {
            DataHashtableUpdated?.Invoke();
        }

        public static void AddText(DateTime date, string text)
        {
            if (Exists(date))
                throw new InvalidOperationException(); // 指定日期的数据已存在
            dataHashtable.Add(date, text);
            storageCount++;
            OnDataHashtableUpdated();
        }

        // 增加文本，但不存储到硬盘
        public static void AddTextWithoutStoringToDisk(DateTime date, string text)
        {
            if (Exists(date))
                throw new InvalidOperationException(); // 指定日期的数据已存在
            dataHashtable.Add(date, text);
            storageCount++;
        }

        public static void UpdateText(DateTime date, string newText)
        {
            if (!Exists(date))
                throw new InvalidOperationException(); // 指定日期的数据不存在
            dataHashtable.Remove(date);
            dataHashtable.Add(date, newText);
            OnDataHashtableUpdated();
        }

        public static bool Exists(DateTime date)
        {
            return dataHashtable.Contains(date);
        }

        public static string GetText(DateTime date)
        {
            if (!Exists(date))
                throw new InvalidOperationException(); // 指定日期的数据不存在
            return dataHashtable[date].ToString();
        }

        public static string GetAllDateAndText()
        {
            StringBuilder allText = new StringBuilder();
            IDictionaryEnumerator en = dataHashtable.GetEnumerator();
            while (en.MoveNext())
            {
                var thisKey = en.Key as DateTime?;
                if (thisKey != null)
                    allText.AppendLine(thisKey?.Year + "-" + thisKey?.Month + "-" + thisKey?.Day);
                allText.AppendLine(en.Value.ToString());
                allText.AppendLine();
            }
            return allText.ToString();
        }

        public static void RemoveText(DateTime date)
        {
            if (!Exists(date))
                throw new InvalidOperationException(); // 指定日期的数据不存在
            dataHashtable.Remove(date);
            storageCount--;
            OnDataHashtableUpdated();
        }

        public static void Clear()
        {
            dataHashtable.Clear();
        }
    }
}
