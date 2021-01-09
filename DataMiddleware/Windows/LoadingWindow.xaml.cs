/*****************************************************

** 作者：董旭阳
** 日期：2019/7/3 9:55:08
** 描述：文件内容或所属项目

** 非文件作者禁止修改或删除文件内容

******************************************************/

using System;
using System.Threading;

namespace DataMiddleware.Windows
{
    /// <summary>
    /// 加载窗口
    /// </summary>
    public partial class LoadingWindow : WindowBase
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private Thread m_Thread;

        /// <summary>
        /// 当前步骤
        /// </summary>
        private int m_Step = 0;

        /// <summary>
        /// 构造
        /// </summary>
        public LoadingWindow()
        {
            InitializeComponent();

            m_Thread = new Thread(() =>
            {
                Thread.Sleep(2000);

                this.Dispatcher.Invoke(() =>
                {
                    this.DialogResult = true;
                });

            });

            m_Thread.Start();
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
