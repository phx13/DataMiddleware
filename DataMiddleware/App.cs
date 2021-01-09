/*****************************************************

** 作者：董旭阳
** 日期：2019/7/3 10:07:33
** 描述：文件内容或所属项目

** 非文件作者禁止修改或删除文件内容

******************************************************/


using System;
using System.Windows;
using DataMiddleware.Windows;

namespace DataMiddleware
{
    public class App
    {
        [STAThread]
        public static void Main()
        {
            LoadingWindow loading = new LoadingWindow();

            bool? result = loading.ShowDialog();

            if (result.Equals(true))
            {
                Application app = new Application();
                app.StartupUri = new Uri("Windows/MainWindow.xaml", UriKind.Relative);
                app.Run();
            }
        }
    }
}
