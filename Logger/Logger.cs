using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace VisionDemo.Logger
{
    public class Logger
    {
        private static string pathFile = Application.StartupPath + "\\Log";
        private static int orderNum = 0;
        public static void WriteLog(string strData)
        {
            //创建文件夹
            StringBuilder strFolder = new StringBuilder();
            strFolder.AppendFormat("{0}\\{1}\\{2}\\",pathFile,DateTime.Now.Year.ToString(),DateTime.Now.Month.ToString());
            if (!Directory.Exists(strFolder.ToString()))
            {
                Directory.CreateDirectory(strFolder.ToString());
            }

            //将数据写入文件
            strFolder.Append(DateTime.Now.ToString("yyyy-MM-dd")+".txt");

            using (StreamWriter stream = File.AppendText(strFolder.ToString()))
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("[{0}][{1}][{2}]{3}", orderNum, DateTime.Now, DateTime.Now.Millisecond.ToString("d4"), strData);
                stream.WriteLine(str.ToString());
            }
            orderNum++;
        }
        
    }

    public class LogWriter:Logger
    {
        private static ListBox myListBox;
        private static  bool BindListBox(ListBox box)
        {
            myListBox = box;

            return true;
        }

        public static void ShowMsgToListBox(string message,bool isShowTime = true)
        {
            WriteLog(message);
            if (myListBox == null)
            {
                return;
            }
            if (isShowTime)
            {
                message += DateTime.Now.ToString("HH:mm:ss")+" ";
            }
            myListBox.Invoke(new Action(()=> {
                myListBox.Items.Insert(0,message);
                myListBox.SelectedIndex = myListBox.Items.Count - 1;
                if (myListBox.Items.Count >=100)
                {
                    myListBox.Items.Clear();
                }
            }));
            

        }
    }
}
