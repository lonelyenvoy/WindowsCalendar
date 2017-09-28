using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowsCalendar
{
    /// <summary>
    /// 记事本硬盘存储相关类
    /// </summary>
    public static class NotepadStorage
    {
        private static bool isInitialized = false; // 将来版本可能会用到的初始化flag

        private const string APPLICATIONDATADIRECTORY = "WindowsCalendar";
        private const string DATAFILENAME = "Notepad";
        private static readonly string applicationDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APPLICATIONDATADIRECTORY, DATAFILENAME);

        private static void DataHashtableUpdatedHandler()
        {
            StoreUpdatedText();
        }

        public static void Initialize()
        {
            // 事件布线
            NotepadHashtable.DataHashtableUpdated += new MyEventHandler(DataHashtableUpdatedHandler);

            // 判断数据文件是否存在
            if (System.IO.File.Exists(applicationDataPath))
            {
                // 读入数据
                FileStream fs = new FileStream(applicationDataPath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                DateTime key = new DateTime();
                string text;

                while (true)
                {
                    try
                    {
                        key = DateTime.Parse(br.ReadString());
                        text = br.ReadString();
                        NotepadHashtable.AddTextWithoutStoringToDisk(key, text);
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }
                }
                br.Close();
            }
            else
            {
                // 创建磁盘文件
                System.IO.File.Create(applicationDataPath);
            }

            isInitialized = true;
        }

        private static void StoreUpdatedText()
        {
            System.IO.File.Delete(applicationDataPath);
            FileStream fs = new FileStream(applicationDataPath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);

            IDictionaryEnumerator e = NotepadHashtable.DataHashTable.GetEnumerator();
            while (e.MoveNext())
            {
                bw.Write(e.Key.ToString());
                bw.Write(e.Value.ToString());
            }
            bw.Close();
        }

    }
}
