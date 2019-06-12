using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Vision_Logger
{
    public class Logger
    {
        private static string _pathFile = Application.StartupPath + "\\Log";
        private static int _orderNum = 0;
        public static void WriteFile(string strData)
        {
            //创建文件夹
            StringBuilder strFolder = new StringBuilder();
            strFolder.AppendFormat("{0}\\{1}\\{2}\\",_pathFile,DateTime.Now.Year.ToString(),DateTime.Now.Month.ToString());
            if (!Directory.Exists(strFolder.ToString()))
            {
                Directory.CreateDirectory(strFolder.ToString());
            }

            //将数据写入文件
            strFolder.Append(DateTime.Now.ToString("yyyy-MM-dd")+".txt");

            using (StreamWriter stream = File.AppendText(strFolder.ToString()))
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("[{0}][{1}][{2}]{3}", _orderNum, DateTime.Now, DateTime.Now.Millisecond.ToString("d4"), strData);
                stream.WriteLine(str.ToString());
            }
            _orderNum++;
        }
        
    }
    //LogWriter类继承自Logger类
    public class WriteLog:Logger
    {
        private static ListBox _myListBox;
        //绑定ListBox
        public  static  bool BindListBox(ListBox box)
        {
            _myListBox = box;

            return true;
        }
        /// <summary>
        /// 显示信息到LIstBox中,可以选择是否显示时间信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isShowTime"></param>
        public static void ShowMsgToListBox(string message,bool isShowTime = true)
        {
            WriteFile(message);
            if (_myListBox == null)
            {
                return;
            }
            if (isShowTime)
            {
                message ="[" +DateTime.Now.ToString("HH:mm:ss")+"]  "+message;
            }
            _myListBox.Invoke(new Action(()=> {
                _myListBox.Items.Insert(0,message);
                _myListBox.SelectedIndex = _myListBox.Items.Count - 1;
                if (_myListBox.Items.Count >=100)
                {
                    _myListBox.Items.Clear();
                }
            }));
            

        }
    }
}
